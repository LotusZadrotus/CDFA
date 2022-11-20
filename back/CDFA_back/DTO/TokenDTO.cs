namespace CDFA_back.DTO
{
    public record TokenDTO
    {
        public string Name { get; set; }
        public string Token { get; set; }
        public TokenDTO(string name, string token)
        {
            Name = name;
            Token = token;
        }
    }
}
