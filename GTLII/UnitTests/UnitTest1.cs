using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GTLII.Controllers;
using GTLII.Services;
using Moq;
namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        BookController bc;
        Mock<BooksRepository> repoMock;
        [TestInitialize]
        public void BeforeRun()
        {
            repoMock = new Mock<BooksRepository>();
            bc = new BookController(repoMock.Object);
        }
        [TestMethod]
        public void TestGetAllBooks()
        {
            //magic shit happening here
        }
    }
}
