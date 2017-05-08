
using GTLII.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTLII.Services
{
    public class BooksRepository : IBooksRepository
    {
        public static List<Book> books;

        public BooksRepository()
        {
            books = new List<Book>();
            Book book1 = new Book()
            {
                Id = 1,
                ISBN = "asdasdasdasd",
                Name = "Name",
                Copies = new List<BookCopy>
                {
                    new BookCopy
                    {
                        Id =1,
                        IsAvailable = true
                    },
                      new BookCopy
                    {
                          Id =2,
                        IsAvailable = true
                    }
                }





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
        public IEnumerable<Book> GetBooks(string name = "")
        {
            if (books == null)
                return null;
            if (name != "")
                return books.Where(b => b.Name.ToLower().Contains(name.ToLower())).ToList();
            else
                return books.ToList();
        }

        public Book GetBook(int id)
        {
            if (books == null)
                return null;
            return books.Find(b => b.Id == id);
            // books.
        }

    }
}
