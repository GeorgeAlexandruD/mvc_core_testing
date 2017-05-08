using GTLII.Services;
using GTLII.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GTLII.Controllers
{

    [Route("api/books/")]
    public class BookCopyController : Controller
    {

        //dependency is done in service
        private IBooksRepository _repo;
        public BookCopyController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{bookId}/copies")]
        public IActionResult GetCopies(int bookId)
        {
            var book = _repo.GetBook(bookId);
            if(book== null)
            {
                return NotFound();
            }
            return Ok(book.Copies);

        }
        [HttpGet("{bookId}/copies/{id}")]
        public IActionResult GetCopy(int bookId, int id)
        {
            var book = _repo.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }
            //provisory 
            if (book.Copies == null)
                return NotFound();
            var copy = book.Copies.FirstOrDefault(c => c.Id == id);
            if (copy == null)
                return NotFound();
            else
                return Ok(copy);
        }
        [HttpPatch("{bookId}/copies/{id}")]
        public IActionResult LoanBook(int bookId, int id, [FromBody] JsonPatchDocument<BookCopyLoanVM> patchDoc)
        {
            /*[{
  "op": "replace",
  "path": "/isAvailable",
  "value": "false"
}]*/
            if(patchDoc == null)
            {
                return BadRequest();
            }
            var book = _repo.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }
            //provisory 
            if (book.Copies == null)
                return NotFound();
            var copy = _repo.GetBook(bookId).Copies.FirstOrDefault(c => c.Id == id);
            if (copy == null)
                return NotFound();
            var copyPatch = new BookCopyLoanVM()
            {

                IsAvailable = copy.IsAvailable
            };
            patchDoc.ApplyTo(copyPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            copy.IsAvailable = copyPatch.IsAvailable;
            return Ok(copy);// change to no content
        }

    }
}