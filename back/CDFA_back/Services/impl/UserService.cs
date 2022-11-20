using CDFA_back.DAL;
using CDFA_back.DTO;
using CDFA_back.Models;
using CDFA_back.Util;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CDFA_back.Services.impl
{
    public class UserService: IUserService
    {
        private readonly IUserRepository _users;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository users, ILogger<UserService> logger)
        {
            _users = users;
            _logger = logger;
        }

        public async Task<TokenDTO> GetTokenAsync(string name, string password)
        {
            try
            {
                var identity = await GetIdentityAsync(name, password);
                if (identity == null)
                {
                    throw new InvalidOperationException("Invalid username or password");
                }

                var now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromDays(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                return new TokenDTO(name, encodedJwt);
            }catch(InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in method GetTokenAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }

        public async Task<UserDTO> GetUserAsync(string name)
        {
            try
            {
                var toReturn = await _users.GetUserByNameAsync(name);
                if(toReturn is null)
                {
                    throw new InvalidOperationException("Can't find user with such name.");
                }
                return new UserDTO(toReturn);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetTokenAsync in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }

        public async Task<UserDTO> RegistrateUser(RegistrateUserDTO user)
        {
            try
            {
                byte[] salt = new byte[128 / 8];
                using (var rngCsp = RandomNumberGenerator.Create())
                {
                    rngCsp.GetNonZeroBytes(salt);
                }
                string hashed = Hash(user.Password, salt);
                var toCreate = new User(user.Name, hashed, Convert.ToBase64String(salt));
                toCreate = await _users.RegistrateUserAsync(toCreate);
                return new UserDTO(toCreate);
            }
            catch (InvalidOperationException e)
            {
                throw new InvalidOperationException(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method RegistrateUser in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                throw new Exception(e.Message); //TODO: Create a ServiceException 
            }
        }
        private static string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));
        }
        private async Task<ClaimsIdentity?> GetIdentityAsync(string login, string password)
        {
            var user = await _users.GetUserByNameAsync(login);
            string hashed = Hash(password, Convert.FromBase64String(user.Salt));
            _logger.LogInformation($"If: {hashed == user.Hashed}");
            if (user.Hashed == hashed)
            {
                var claims = new List<Claim>
                {
                    new Claim(type:ClaimsIdentity.DefaultNameClaimType, value: user.Name.ToString())
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }
    }
}
