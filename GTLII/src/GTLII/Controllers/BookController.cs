using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTLII.Entities;
using Microsoft.AspNetCore.Mvc;
using GTLII.Services;
using GTLII.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GTLII.Controllers
{

    [Route("api/books")]
    public class BookController : Controller
    {
        
        //dependency is done in service


        private IBooksRepository _repo;
        public BookController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetBooks( string name="")
        {
            //repository

            List<BookVM> finalResults = new List<BookVM>();
            var results = _repo.GetBooks(name);
            
            if (results == null)
            {
                return NotFound();
            }
            foreach (var r in results)
            {
                BookVM b = new BookVM()
                {
                    Id = r.Id,
                    Name= r.Name,
                    ISBN=r.ISBN
                  
                };
                finalResults.Add(b);
            }
          
                return Ok(finalResults);
        }
        [HttpGet("title/{title}")]
        public IActionResult GetBooksByTitle(string title)
        {
            //repository

            List<BookVM> finalResults = new List<BookVM>();
            var results = _repo.GetBooks(title);

            if (results == null)
            {
                return NotFound();
            }
            foreach (var r in results)
            {
                BookVM b = new BookVM()
                {
                    Id = r.Id,
                    Name = r.Name,
                    ISBN = r.ISBN
                };
                finalResults.Add(b);
            }

            return Ok(finalResults);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBook(int id)
        {
            var result = _repo.GetBook(id);
            if (result == null)
            {
                return NotFound();
            }
            BookVM b = new BookVM()
            {
                Id = result.Id,
                ISBN = result.ISBN,
                Name = result.Name
            };
                return Ok(b);

        }

        //[HttpPost("{id}")]
        //public IActionResult GetBook(BookVM book)
        //{
        //var result = _repo.GetBook(id);
        //if (result == null)
        //{
        //    return NotFound();
        //}
        //BookVM b = new BookVM()
        //{
        //    Id = result.Id,
        //    ISBN = result.ISBN,
        //    Name = result.Name
        //};
        // return Ok();

        // }
    }
}