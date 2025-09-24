using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL.Models;

public partial class BorrowedBook
{
    [Key]
    public int BorrowId { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime BorrowDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ReturnDate { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BorrowedBooks")]
    public virtual Book Book { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("BorrowedBooks")]
    public virtual User User { get; set; } = null!;
}
