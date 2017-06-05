using GTLII.Services;
using GTLII.ViewModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GTLII.Entities;

namespace GTLII.Controllers
{

    [Route("api/books/")]
    public class BookCopyController : Controller
    {

        //dependency is done in service
        private readonly IBooksRepository _repo;
        public BookCopyController(IBooksRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{bookId}/copies")]
        public IActionResult GetCopies(int bookId)
        {
            var book = _repo.GetBook(bookId);
            if (book == null)
                return NotFound();
            var copies = book.Copies ?? new List<BookCopy>();
            return Ok(copies.Select(copy => new BookCopyVM(copy)));

        }
        [HttpGet("{bookId}/copies/{id}")]
        public IActionResult GetCopy(int bookId, int id)
        {
/*            var book = _repo.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }
            //provisory 
            if (book.Copies == null)
                return NotFound();*/
            var copy = _repo.GetCopy(bookId, id);
            if (copy == null)
                return NotFound();
            var copyVm = new BookCopyVM
            {
                Id = copy.Id,
                IsAvailable = copy.IsAvailable
            };
            return Ok(copyVm);
        }
        [HttpPatch("{bookId}/copies/{id}")]
        public IActionResult LoanBook(int bookId, int id, [FromBody] JsonPatchDocument<BookCopyLoanVM> patchDoc)
        {

            if (patchDoc == null)
            {
                return BadRequest();
            }
            var book = _repo.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }
           
            var copy = _repo.GetCopy(bookId, id);
            if (copy == null)
                return NotFound();
            var copyPatch = new BookCopyLoanVM()
            {
                IsAvailable = copy.IsAvailable
            };
            patchDoc.ApplyTo(copyPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
             bool result =_repo.LoanCopy(bookId,id);
            if (result == false)
                return BadRequest();
            return NoContent();// change to no content
        }

        [HttpPatch("{bookId}/copies2/{id}")]
        public IActionResult LoanBook2(int bookId, int id, [FromBody] JsonPatchDocument<BookCopyLoanVM> patchDoc)
        {

            if (patchDoc == null)
            {
                return BadRequest();
            }
            var book = _repo.GetBook(bookId);
            if (book == null)
            {
                return NotFound();
            }

            var copy = _repo.GetCopy(bookId, id);
            if (copy == null)
                return NotFound();
            var copyPatch = new BookCopyLoanVM()
            {
                IsAvailable = copy.IsAvailable
            };
            var beforePatched = new BookCopyLoanVM
            {
                IsAvailable = copy.IsAvailable
            };
            patchDoc.ApplyTo(copyPatch, ModelState);
            if (!ModelState.IsValid)
                return BadRequest();
            bool result = _repo.LoanCopy(bookId, id);
            if (result == false)
                return BadRequest();
            var model = new
            {
                original = beforePatched,
                patched = copyPatch
            };

            return Ok(model);
        }

    }
}