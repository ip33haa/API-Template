using System.Data;

namespace API.Domain.Entity
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Guid? RoleId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }


        // Navigation property for one-to-many relationship with Role
        public Role? Role { get; set; }
    }
}
