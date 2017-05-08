using GTLII.Controllers;
using GTLII.Entities;
using GTLII.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class BookControllerIntegrationTest
    {
        
        [Fact]
        public void GetAllBooks( )
        {
            BooksRepository repo = new BooksRepository();
            BookController bc = new BookController(repo);
            var actionResult = bc.GetBooks();
            Assert.IsType<OkObjectResult>(actionResult);
        }
    }
}
