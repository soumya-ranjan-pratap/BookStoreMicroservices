using BookService.Data;
using BookService.Model;
using BookService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public BookController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(dbContext.Books.ToList());
        }

        [HttpGet("{Title}")]
        public ActionResult<Book> GetBook(string Title)
        {
            var book = dbContext.Books.FirstOrDefault(x => x.Title == Title);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> PostBook(AddBooks book)
        {
            var newbook = new Book()
            {
                Title = book.Title,
                AuthorName = book.AuthorName
            };

            dbContext.Books.Add(newbook);
            dbContext.SaveChanges();
            return CreatedAtAction(nameof(AddBooks), new { title = book.Title }, book);
        }

        [HttpPut("{Title}")]
        public ActionResult UpdateBook(string Title, AddBooks book)
        {
            if (Title != book.Title)
            {
                return BadRequest();
            }
            var ubook = dbContext.Books.FirstOrDefault(x => x.Title == Title);
            if (ubook != null)
            {
                ubook.Title = book.Title;
                ubook.AuthorName = book.AuthorName;
                dbContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{Title}")]
        public ActionResult DeleteBook(string Title)
        {
            var delBook = dbContext.Books.FirstOrDefault(x=>x.Title == Title);
            if(delBook != null)
            {
                dbContext.Books.Remove(delBook);
                dbContext.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
