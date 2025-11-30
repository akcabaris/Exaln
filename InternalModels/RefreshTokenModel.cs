namespace Exaln.InternalModels
{
    public class RefreshTokenModel
    {
        public string Token { get; set; }
        public string UserId { get; set;  }
        public string? IpAdress {  get; set; }
        public string? Device {  get; set; }
        public DateTime CreatedAt { get; set; }
        public short ExpireDays { get; set; }
    }
}
