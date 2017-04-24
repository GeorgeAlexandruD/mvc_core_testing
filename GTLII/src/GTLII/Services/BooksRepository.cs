
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

          
            books.Add(book1);
            books.Add(book2);
            books.Add(book3);
        }
        public IEnumerable<Book> GetBooks()
        {
            return books.ToList();
        }

        public Book GetBook(int id)
        {
            return books.Find(b => b.Id == id);
        }
    }
}
