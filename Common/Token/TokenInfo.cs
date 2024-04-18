using Tokens;

namespace Tokens
{
    public class TokenInfo
    {
        public Guid? Id { get; set; }
        public string? UserId { get; set; }
        public string? LoginId { get; set; }

        public BaseModel? BaseModel { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ExpiryDate { get; set; }
        public string? ip_address { get; set; }
        public string? browser_name { get; set; }
        public string? browser_version { get; set; }
        public string? server_Value { get; set; }
       
    }
}
