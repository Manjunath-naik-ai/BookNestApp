using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestDAL.Models;


namespace BookNestDAL
{
    public  class AuthRepo
    {
        public BookLibraryDbContext _context;
        public AuthRepo()
        {
            _context = new BookLibraryDbContext();
        }
        
        // Register method
        public string Register(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (existingUser != null) return "Email already exists";

            user.PasswordHash = user.PasswordHash;
            user.Role = "User"; 
            user.CreatedAt = DateTime.Now;

            _context.Users.Add(user);
            _context.SaveChanges();
            return "User registered successfully";
        }
        //Login Method

        public User Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            bool isValid = password == user.PasswordHash;
            if (!isValid) return null;


            return user;
        }
        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

    }
}

