using GTLII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTLII.Services
{
    public  interface IBooksRepository
    {
        IEnumerable<Book> GetBooks(string name="");
        Book GetBook(int id);
        
    }
}
