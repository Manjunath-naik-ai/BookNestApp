using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL.Models;

[Index("Email", Name = "UQ__Users__A9D1053452C217C5", IsUnique = true)]
public partial class User

{
    [Key]
    public int UserId { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    public string Email { get; set; } = null!;

    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [StringLength(20)]
    public string Role { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}
