using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestDAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookNestDAL
{
    public class UserRepo
    {

        public BookLibraryDbContext _context;
        public UserRepo()
        {
            _context = new BookLibraryDbContext();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                .Select(u => new User
                {
                    UserId = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    Role = u.Role
                })
                .ToList();
        }

  
        public User GetUserById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

  
        public List<BorrowedBook> GetBorrowedBooksByUser(int userId)
        {
            return _context.BorrowedBooks
                .Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .ToList();
        }

 
        public User AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

      
        public bool DeleteUser(int userId)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            _context.SaveChanges();
            return true;
        }
    }

}

