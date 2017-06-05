using GTLII.Controllers;
using GTLII.Entities;
using GTLII.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTLII.ViewModels;
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
            //Arrange
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

            //Act
            var actionResult = bc.GetBooks();

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }
        [Fact]
        public void GetBookRightId()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name"
            };
               
            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);

            bc = new BookController(repoMock.Object);

            //Act
            var actionResult = bc.GetBook(1);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);

        }

        [Fact]
        public void GetBookDataTypeForRightId()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name"
            };

            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);

            bc = new BookController(repoMock.Object);

            //Act
            var actionResult = bc.GetBook(1);
            var okObjectResult = (OkObjectResult) actionResult;

            //Assert
            Assert.IsType<BookVM>(okObjectResult.Value);
        }

        [Fact]
        public void GetBookDataForRightId()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name"
            };

            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);

            bc = new BookController(repoMock.Object);

            //Act
            var actionResult = bc.GetBook(1);
            var okObjectResult = (OkObjectResult)actionResult;
            var result = (BookVM) okObjectResult.Value;

            //Assert
            Assert.Equal(result.ISBN, mockResult.ISBN);
            Assert.Equal(result.Id, mockResult.Id);
            Assert.Equal(result.Name, mockResult.Name);
        }

        [Fact]
        public void GetBookWrongId()
        {
            //Arrange
            repoMock = new Mock<IBooksRepository>();

            bc = new BookController(repoMock.Object);

            //Act
            var actionResult = bc.GetBook(1);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void CheckRepositoryDataNotFound()
        {
            //Arrange
            repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetBooks(It.IsAny<string>())).Returns(null as IEnumerable<Book>);

            bc = new BookController(repoMock.Object);

            //Act 
            var actionResult = bc.GetBooks("Some magnificent name");

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }
        [Fact]
        public void GetBooksByTitleNoBooks()
        {
            //Arrange
            var mockResult = new List<Book>();

            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBooks("Little Mermeid")).Returns(mockResult);
            var bcc = new BookController(repoMock.Object);

            //Act 
            var actionResult = bcc.GetBooksByTitle("Little Mermeid");

            //Assert 
            Assert.IsType<OkObjectResult>(actionResult);
        }
        [Fact]
        public void GetBooksByTitleAreBooks()
        {
            //Arrange
            var mockResult = new List<Book>
            {
                new Book()
                {
                    Id = 1,
                    ISBN = "asd",
                    Name = "Little Mermeid",
                    Copies = new List<BookCopy> {new BookCopy {Id = 1, IsAvailable = false}}
                }
            };

            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBooks("Little Mermeid")).Returns(mockResult);
            var bcc = new BookController(repoMock.Object);

            //Act 
            var actionResult = bcc.GetBooksByTitle("Little Mermeid");

            //Assert 
            Assert.IsType<OkObjectResult>(actionResult);
        }
        [Fact]
        public void GetBooksByTitleNull()
        {
            //Arrange
            IEnumerable<Book> mockResult = null;

            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBooks("Little Mermeid")).Returns(mockResult);
            var bcc = new BookController(repoMock.Object);

            //Act 
            var actionResult = bcc.GetBooksByTitle("Little Mermeid");

            //Assert 
            Assert.IsType<NotFoundResult>(actionResult);
        }
    }
}
