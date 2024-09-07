using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public int UserId { get; set; }

    public int RoleId { get; set; }

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public string salt { get; set; } = string.Empty;

    [Required]
    public int IsLoggedIn { get; set; }

    [Required]
    public DateTime TimeLoggedIn { get; set; }

    [Required]
    public int IsLoggedOut { get; set; }

    [Required]
    public DateTime TimeLoggedOut { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    [Required]
    public string UpdatedBy { get; set; }

    // Navigation property for the many-to-many relationship
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    public string RoleName { get; set; } = string.Empty;

    public int AccessLevel { get; set; }

    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public DateTime UpdatedOn { get; set; }

    [Required]
    public string CreatedBy { get; set; }

    [Required]
    public string UpdatedBy { get; set; }

    // Navigation property for the many-to-many relationship
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class UserRole
{
    [Key]
    public int UserRoleId { get; set; }

    // Foreign keys to establish the many-to-many relationship
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role Role { get; set; }
}
