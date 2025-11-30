namespace Exaln.DTO.AccountDTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
        public UserDTO User { get; set; } = null!;
    }
}
