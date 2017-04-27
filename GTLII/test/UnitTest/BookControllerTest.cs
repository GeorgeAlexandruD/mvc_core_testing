using GTLII.Controllers;
using GTLII.Entities;
using GTLII.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class BookControllerTest
    {
        Mock<IBooksRepository> repoMock;
        BookController bc;
        [Fact]
        public void GetAllBooks()
        {
            List<Book> booksMock = new List<Book>()
            {
                new Book()
                {
                    Id=1,
                    ISBN="asd",
                    Name="name"
                },
                new Book()
                {
                    Id=2,
                    ISBN="asdd",
                    Name="nana"
                }

            };
            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBooks(It.IsAny<string>())).Returns(booksMock);

            bc = new BookController(repoMock.Object);
            // fails at converting action result to books, maybe json... dunno
            List<Book> books = bc.GetBooks() as List<Book>;
            Assert.Equal(books.Count, 2);
            Assert.Equal(books.Find(b => b.Id == 1).Name, "name");
        }
    }
}
