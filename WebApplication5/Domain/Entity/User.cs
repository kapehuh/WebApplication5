using WebApplication5.Abstractions;

namespace WebApplication5.Domain.Entity
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
    }
}
