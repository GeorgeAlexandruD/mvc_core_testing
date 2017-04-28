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
    public class RepositoryControllerTest
    {
        Mock<IBooksRepository> repoMock;

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
            List<Book> books = repoMock.Object.GetBooks(It.IsAny<string>()).ToList();


            Assert.Equal(books.Count, 2);
            Assert.Equal(books.Find(b => b.Id == 1).Name, "name");
        }
        [Fact]
        public void GetBook()
        {
            Book result = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name"
            };

            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(result);


            repoMock.Object.GetBook(1);
            Assert.Equal(repoMock.Object.GetBook(1).ISBN, "asd");
        }



    }
}
