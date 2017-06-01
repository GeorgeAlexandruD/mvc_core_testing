using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTLII.Controllers;
using GTLII.Entities;
using GTLII.Services;
using GTLII.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace UnitTest
{
    public class BookCopyControllerTest
    {
        [Fact]
        public void LoanBookBadRequest()
        {
            var repoMock = new Mock<IBooksRepository>();
            var bcc = new BookCopyController(repoMock.Object);

            //Act
            var actionResult = bcc.LoanBook(6, 3, null);

            //Assert
            Assert.IsType<BadRequestResult>(actionResult);
        }

        [Fact]
        public void LoanBookNotFound()
        {
            var repoMock = new Mock<IBooksRepository>();
            var bcc = new BookCopyController(repoMock.Object);

            //Act 
            var actionResult = bcc.LoanBook(3, 3, new JsonPatchDocument<BookCopyLoanVM>());

            //Assert 
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void LoanCopyNotFound()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name"
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);
            repoMock.Setup(b => b.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(null as BookCopy);
            var bcc = new BookCopyController(repoMock.Object);

            //Act 
            var actionResult = bcc.LoanBook(1, 3, new JsonPatchDocument<BookCopyLoanVM>());

            //Assert 
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void LoanNotValidBadRequest()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name",
                Copies = new List<BookCopy> { new BookCopy { Id = 1, IsAvailable = false } }
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);
            repoMock.Setup(b => b.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(mockResult.Copies.First);
            repoMock.Setup(mock => mock.LoanCopy(1, 1)).Returns(false);
            var bcc = new BookCopyController(repoMock.Object);

            //Act 
            var actionResult = bcc.LoanBook(1, 1, new JsonPatchDocument<BookCopyLoanVM>());

            //Assert 
            Assert.IsType<BadRequestResult>(actionResult);
        }
        [Fact]
        public void LoanSuccess()
        {
            //Arrange
            Book mockResult = new Book()
            {
                Id = 1,
                ISBN = "asd",
                Name = "name",
                Copies = new List<BookCopy> { new BookCopy { Id = 1, IsAvailable = false } }
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(b => b.GetBook(1)).Returns(mockResult);
            repoMock.Setup(b => b.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(mockResult.Copies.First);
            repoMock.Setup(mock => mock.LoanCopy(1, 1)).Returns(true);
            var bcc = new BookCopyController(repoMock.Object);

            //Act 
            var actionResult = bcc.LoanBook(1, 1, new JsonPatchDocument<BookCopyLoanVM>());

            //Assert 
            Assert.IsType<NoContentResult>(actionResult);
        }

        [Fact]
        public void GetCopyNotFound()
        {
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(null as BookCopy);
            var bcc = new BookCopyController(repoMock.Object);

            //Act
            var actionResult = bcc.GetCopy(1, 1);

            //Assert 
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void GetCopyFound()
        {
            var bookMock = new BookCopy
            {
                Id = 1,
                IsAvailable = true
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(bookMock);
            var bcc = new BookCopyController(repoMock.Object);

            //Act
            var actionResult = bcc.GetCopy(1, 1);

            //Assert 
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void GetCopyDataTypeCorrectness()
        {
            var bookMock = new BookCopy
            {
                Id = 1,
                IsAvailable = true
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(bookMock);
            var bcc = new BookCopyController(repoMock.Object);

            //Act
            var actionResult = (OkObjectResult) bcc.GetCopy(1, 1);

            //Assert 
            Assert.IsType<BookCopyVM>(actionResult.Value);
        }

        [Fact]
        public void GetCopyDataCorrectness()
        {
            var bookMock = new BookCopy
            {
                Id = 1,
                IsAvailable = true
            };
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetCopy(It.IsAny<int>(), It.IsAny<int>())).Returns(bookMock);
            var bcc = new BookCopyController(repoMock.Object);

            //Act
            var actionResult = (OkObjectResult)bcc.GetCopy(1, 1);
            var result = (BookCopyVM) actionResult.Value;

            //Assert 
            Assert.Equal(result.Id, bookMock.Id);
            Assert.Equal(result.IsAvailable, bookMock.IsAvailable);
        }

        [Fact]
        public void GetCopiesBookNotFound()
        {
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetBook(It.IsAny<int>())).Returns(null as Book);
            var bcc = new BookCopyController(repoMock.Object);

            var actionResult = bcc.GetCopies(55);

            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void GetCopiesOkResult()
        {
            var bookMock = new Book();
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetBook(It.IsAny<int>())).Returns(bookMock);
            var bcc = new BookCopyController(repoMock.Object);

            var actionResult = bcc.GetCopies(1);

            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void GetCopiesDataType()
        {
            var bookMock = new Book();
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetBook(It.IsAny<int>())).Returns(bookMock);
            var bcc = new BookCopyController(repoMock.Object);

            var actionResult = (OkObjectResult) bcc.GetCopies(1);

            Assert.IsAssignableFrom<IEnumerable<BookCopyVM>>(actionResult.Value);
        }

        [Fact]
        public void GetCopiesDataCorrectness()
        {
            var dummyCopies = new List<BookCopy>
            {
                new BookCopy {Id = 1, IsAvailable = true},
                new BookCopy {Id = 2, IsAvailable = false},
                new BookCopy {Id = 5556, IsAvailable = true}
            };
            var dummyBook = new Book {Copies = dummyCopies};
            var repoMock = new Mock<IBooksRepository>();
            repoMock.Setup(mock => mock.GetBook(It.IsAny<int>())).Returns(dummyBook);
            var bcc = new BookCopyController(repoMock.Object);
            var expectedResult = dummyCopies.Select(dc => new BookCopyVM(dc));

            var actionResult = (OkObjectResult)bcc.GetCopies(1);
            var result = (IEnumerable<BookCopyVM>) actionResult.Value;

            Assert.Equal(expectedResult, result);
        }

    }
}
