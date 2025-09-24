using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookNestDAL;

namespace BookNestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase

    {
        private readonly UserRepo _userRepo;

        public UsersController()
        {
            _userRepo = new UserRepo();
        }

       
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepo.GetAllUsers();
            return Ok(users);
        }

      
        [HttpGet("{id}/borrowed")]
        public IActionResult GetBorrowedBooks(int id)
        {
            var borrowedBooks = _userRepo.GetBorrowedBooksByUser(id);
            return Ok(borrowedBooks);
        }
    }
}

