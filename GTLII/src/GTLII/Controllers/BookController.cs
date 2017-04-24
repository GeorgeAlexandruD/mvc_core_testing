using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GTLII.Services;
using GTLII.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GTLII.Controllers
{

    [Route("api/books")]
    public class BookController : Controller
    {
        public string[] a;
        //dependency is done in service
        private IBooksRepository _repo;
        public BookController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            //repository

            List<BookVM> finalResults = new List<BookVM>();
            var results = _repo.GetBooks();
            foreach(var r in results)
            {
                BookVM b = new BookVM()
                {
                    Id = r.Id,
                    Name= r.Name,
                    ISBN=r.ISBN
                };
                finalResults.Add(b);
            }
            if (finalResults == null)
                return NotFound();
            else
                return Ok(finalResults);

        }
        [HttpGet("{id}")]
        public IActionResult GetBooks(int id)
        {
            a = new string[] { "value1", "value2" };
            var book = a[id];
            if (book == null)
                return NotFound();
            else
                return Ok(book);

        }
    }
}