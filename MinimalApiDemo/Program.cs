using Microsoft.AspNetCore.Mvc;
using MinimalApiDemo.Data;
using MinimalApiDemo.Models;
using MinimalApiDemo.Models.DTO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapGet("/hello", () => "world");
//app.MapPost("/hello", () => "new world");
//app.MapGet("/error", () => {
//    return Results.BadRequest("Exception");
//});
//app.MapGet("/hello/{name string}", (string name) => {
//    return Results.Ok($"hello {name}");
//});
//app.MapGet("/hellonumber/{id int}", (int id) =>
//{
//    return Results.Ok($"hello number {id}");
//});

app.MapGet("/api/v1/books", (ILogger<Program> _logger) =>
{
    _logger.Log(LogLevel.Information, "getting all books");
    return Results.Ok(BookStore.books);
}).WithName("GetBooks").Produces<IEnumerable<Book>>(200);

app.MapGet("/api/v1/book/{id int}", (int id) =>
{
    return Results.Ok(BookStore.books.Find(book => book.Id == id));
}).WithName("GetBook").Produces<Book>(200);

app.MapPost("/api/v1/book/", ([FromBody] BookCreateDTO bookCreateDTO) =>
{
    if ((bookCreateDTO.Title != string.Empty) && (BookStore.books.Find(b => b.Title == bookCreateDTO.Title) == null))
    {
        Book book = new Book
        {
            Id = BookStore.books.OrderByDescending(book => book.Id).FirstOrDefault().Id + 1,
            Title = bookCreateDTO.Title,
            IsActive = bookCreateDTO.IsActive,
        };
        BookStore.books.Add(book);

        return Results.Created($"/api/v1/book/{book.Id}", book);
        //return Results.CreatedAtRoute("GetBook", book, book);
    }
    return Results.BadRequest("Invalid data");
}).WithName("PostBook").Produces<Book>(201).Produces(400);

app.MapPut("/api/v1/book/{id int}", (int id) =>
{
    return Results.Ok(BookStore.books.Find(book => book.Id == id));
});

app.MapDelete("/api/v1/book/{id int}", (int id) =>
{
    var book = BookStore.books.Find(book => book.Id == id);
    return Results.Ok(BookStore.books.Remove(book));
});

app.UseHttpsRedirection();

app.Run();



