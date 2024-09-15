using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class User
{
    [Key]
    public int UserId { get; set; }

    public int RoleId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string UserName { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string Email { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string Password { get; set; } = string.Empty;

    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string salt { get; set; } = string.Empty;

    [Required]
    public int IsLoggedIn { get; set; }

    [Required]
    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime TimeLoggedIn { get; set; }

    [Required]
    public int IsLoggedOut { get; set; }

    [Required]
    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime TimeLoggedOut { get; set; }

    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime CreatedOn { get; set; }

    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime UpdatedOn { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string CreatedBy { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string UpdatedBy { get; set; }

    // Navigation property for the many-to-many relationship
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string RoleName { get; set; } = string.Empty;

    public int AccessLevel { get; set; }

    [Required]
    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime CreatedOn { get; set; }

    [Required]
    [Column(TypeName = "DATETIME")] // MySQL-compatible datetime type
    public DateTime UpdatedOn { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
    public string CreatedBy { get; set; }

    [Required]
    [Column(TypeName = "VARCHAR(255)")] // MySQL-compatible string type
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
