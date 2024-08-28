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
        public ActionResult <IEnumerable<Book>> GetBooks()
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
        public ActionResult UpdateBook(AddBooks book)
        {
        }
    }
}
