using CDFA_back.DTO;
using CDFA_back.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CDFA_back.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController: ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult<TokenDTO>> GetToken(string name, string password)
        {
            try
            {
                var toReturn = await _userService.GetTokenAsync(name, password);
                return new JsonResult(toReturn);
            }catch(InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch(Exception e)
            {
                _logger.LogError($"Error in method GetToken in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }

        [HttpGet]
        [Route("[action]")]
        [Authorize]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            try
            {
                var user_id = HttpContext.User.Claims.FirstOrDefault()?.Value;
                if(user_id is null)
                {
                    return new BadRequestResult();
                }
                var toReturn = await _userService.GetUserAsync(user_id);
                return new OkObjectResult(toReturn);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method GetUser in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<UserDTO>> RegistrateUser([FromBody]RegistrateUserDTO user)
        {
            try
            {
                var toReturn = await _userService.RegistrateUser(user);
                return new JsonResult(toReturn);
            }
            catch (InvalidOperationException e)
            {
                return new BadRequestObjectResult(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in method RegistrateUser in {this.GetType()}{Environment.NewLine} Error: {e.Message}{Environment.NewLine} Stack Trace: {e.StackTrace}", e.InnerException);
                return new StatusCodeResult(500);
            }
        }
    }
}
