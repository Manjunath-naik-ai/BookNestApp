using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookNestDAL;
using BookNestDAL.Models;
using System.Security.Claims;

namespace BookNestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookNestRepo _bookRepo;

        public BooksController()
        {
            _bookRepo = new BookNestRepo();
        }

        // GET: /api/books
        [HttpGet]
        [Authorize] // Any logged-in user
        public IActionResult GetBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return Ok(books);
        }

        // GET: /api/books/{id}
        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetBook(int id)
        {
            var book = _bookRepo.GetBookById(id);
            if (book == null) return NotFound("Book not found");
            return Ok(book);
        }

        // POST: /api/books (Admin only)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook([FromBody] Book book)
        {
            var addedBook = _bookRepo.AddBook(book);
            return Ok(addedBook);
        }

        // PUT: /api/books/{id} (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            var updated = _bookRepo.UpdateBook(id, book);
            if (updated == null) return NotFound("Book not found");
            return Ok(updated);
        }

        // DELETE: /api/books/{id} (Admin only)
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            var success = _bookRepo.DeleteBook(id);
            if (!success) return NotFound("Book not found");
            return Ok("Book deleted successfully");
        }

        // POST: /api/books/borrow/{bookId} (User only)
        [HttpPost("borrow/{bookId}")]
        [Authorize]
        public IActionResult BorrowBook(int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = _bookRepo.BorrowBook(bookId, userId);
            if (result.Contains("not available")) return BadRequest(result);
            return Ok(result);
        }

        // POST: /api/books/return/{bookId} (User only)
        [HttpPost("return/{bookId}")]
        [Authorize]
        public IActionResult ReturnBook(int bookId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = _bookRepo.ReturnBook(bookId, userId);
            if (result.Contains("No borrowed")) return BadRequest(result);
            return Ok(result);
        }
    }
}
