using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookNestDAL.Models;

namespace BookNestDAL
{
    public  class BookNestRepo
    {
       public  BookLibraryDbContext _context;
        public BookNestRepo()
        {
            _context = new BookLibraryDbContext();
        }
        // Add methods for CRUD operations here
        //
    }
}
