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
        var books = context.Books;
        return books;
    }
}