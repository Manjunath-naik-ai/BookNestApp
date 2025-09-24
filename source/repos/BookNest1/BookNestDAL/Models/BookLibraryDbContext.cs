using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL.Models;

public partial class BookLibraryDbContext : DbContext
{
    public BookLibraryDbContext()
    {
    }

    public BookLibraryDbContext(DbContextOptions<BookLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BorrowedBook> BorrowedBooks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BookLibraryDB;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Books__3DE0C2070EF9BC4D");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<BorrowedBook>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__Borrowed__4295F83F9963BF68");

            entity.Property(e => e.BorrowDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Book).WithMany(p => p.BorrowedBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowedB__BookI__3E52440B");

            entity.HasOne(d => d.User).WithMany(p => p.BorrowedBooks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BorrowedB__UserI__3D5E1FD2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CD6AA7383");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
