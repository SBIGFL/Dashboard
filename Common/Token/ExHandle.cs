using Tokens;

namespace Common
{
    public class ExHandle
    {
        public BaseModel? BaseModel { get; set; }
        public Guid? Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Message { get; set; }
        public string? Source { get; set; }
        public Exception? InnerException { get; set; }
        public string? API { get; set; }
    
    }
}
