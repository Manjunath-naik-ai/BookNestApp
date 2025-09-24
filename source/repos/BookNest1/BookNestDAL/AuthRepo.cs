using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestDAL.Models;

namespace BookNestDAL
{
    internal class AuthRepo
    {
        public BookLibraryDbContext _context;
        public AuthRepo()
        {
            _context = new BookLibraryDbContext();
        }
        // Add methods for CRUD
    }
}
