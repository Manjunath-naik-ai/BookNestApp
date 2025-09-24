using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL
{
    public  class BookNestRepo
    {
       public  BookLibraryDbContext _context;
        public BookNestRepo()
        {
            _context = new BookLibraryDbContext();
        }
     
        // Get all books 
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        // Get single book by id
        public Book GetBookById(int id)
        {
            return _context.Books.FirstOrDefault(b => b.BookId == id);
        }

        // Add new book (Admin)
        public Book AddBook(Book book)
        {
            book.AvailableCopies = book.TotalCopies;
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }

        // Update book details (Admin)
        public Book UpdateBook(int id, Book updatedBook)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null) return null;

            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.TotalCopies = updatedBook.TotalCopies;
            book.AvailableCopies = updatedBook.AvailableCopies;

            _context.SaveChanges();
            return book;
        }

        // Delete book (Admin)
        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null) return false;

            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        // Borrow book (User)
        public string BorrowBook(int bookId, int userId)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            if (book == null || book.AvailableCopies <= 0) return "Book not available";

            book.AvailableCopies--;

            _context.BorrowedBooks.Add(new BorrowedBook
            {
                BookId = bookId,
                UserId = userId,
                BorrowDate = DateTime.Now
            });

            _context.SaveChanges();
            return "Book borrowed successfully";
        }

        // Return book (User)
        public string ReturnBook(int bookId, int userId)
        {
            var borrowed = _context.BorrowedBooks
                .FirstOrDefault(b => b.BookId == bookId && b.UserId == userId && b.ReturnDate == null);

            if (borrowed == null) return "No borrowed book found";

            borrowed.ReturnDate = DateTime.Now;

            var book = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            book.AvailableCopies++;

            _context.SaveChanges();
            return "Book returned successfully";
        }

        // Get borrowed books by user (for user or admin view)
        public List<BorrowedBook> GetBorrowedBooksByUser(int userId)
        {
            return _context.BorrowedBooks
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .ToList();
        }
    }
}

