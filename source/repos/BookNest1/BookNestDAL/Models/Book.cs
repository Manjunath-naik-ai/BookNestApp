using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL.Models;

public partial class Book
{
    [Key]
    public int BookId { get; set; }

    [StringLength(255)]
    public string Title { get; set; } = null!;

    [StringLength(100)]
    public string Author { get; set; } = null!;

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Book")]
    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; } = new List<BorrowedBook>();
}
