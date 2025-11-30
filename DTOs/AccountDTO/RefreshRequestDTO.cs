namespace Exaln.DTOs.AccountDTO
{
    public class RefreshRequestDTO
    {
        public string RefreshToken { get; set; } = string.Empty;
        public string UserId { get; set; }= string.Empty; 
    }
}
