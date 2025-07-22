using Microsoft.AspNetCore.Mvc;
using SimpleWebApi.Data;
using SimpleWebApi.Entities;

namespace SimpleWebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookContext context;

    public BooksController(BookContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> List()
    {
        var books = new { items = context.Books };
        return Ok(books);
    }
    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var book = context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    [HttpPost]
    public void Post([FromBody] Book book)
    {
        context.Books.Add(book);
        context.SaveChanges();
    }
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] Book book)
    {
        var target = context.Books.Find(id);
        if (target == null)
        {
            return;
        }
        target.Title = book.Title;
        target.Author = book.Author;
        context.SaveChanges();
    }
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
        var book = context.Books.Find(id);
        if (book == null)
        {
            return;
        }
        context.Books.Remove(book);
        context.SaveChanges();
    }
}