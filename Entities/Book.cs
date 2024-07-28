namespace SimpleWebApi.Entities;
public class Book
{
    public int BookId { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}