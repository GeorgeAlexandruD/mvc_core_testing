using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace GTLII.Controllers
{

    [Route("api/books")]
    public class BookController : Controller
    {
        public string[] a;

        [HttpGet]
        public IActionResult GetBooks()
        {
            //repository 
            a = new string[] { "value1", "value2" };
            if (a == null)
                return NotFound();
            else
                return Ok(a);

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