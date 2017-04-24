
using GTLII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTLII.Services
{
    public class BooksRepository : IBooksRepository
    {
        public List<Book> books = new List<Book>();

        public BooksRepository()
        {
           
            Book book1 = new Book()
            {
                Id = 1,
                ISBN = "asdasdasdasd",
                Name = "Name"

            };
            Book book2 = new Book()
            {
                Id = 2,
                ISBN = "dsaddasdd",
                Name = "Name2"

            };
            Book book3 = new Book()
            {
                Id = 3,
                ISBN = "oipiojlkoh",
                Name = "Name3"

            };
            Book book4 = new Book()
            {
                Id = 4,
                ISBN = "oipiojlkoh",
                Name = "star wars"

            };

            books.Add(book1);
            books.Add(book2);
            books.Add(book3);
            books.Add(book4);
        }
        public IEnumerable<Book> GetBooks(string name)
        {
            if(name!="")
                return books.Where(b => b.Name.ToLower().Contains(name.ToLower())).ToList();
            else
            return books.ToList();
        }

        public Book GetBook(int id)
        {
            return books.Find(b => b.Id == id);
           // books.
        }
       
    }
}
