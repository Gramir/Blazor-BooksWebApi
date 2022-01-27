using Booksv2.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Booksv2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public BookController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net/");
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var books = await _httpClient.GetFromJsonAsync<List<Book>>("api/v1/Books");

            if (books == null)
                return NotFound();

            return Ok(books);

        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            if (id == 0)
                return BadRequest();

            var book = await _httpClient.GetFromJsonAsync<Book>($"api/v1/Books/{id}");

            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> PostBook(Book book)
        {
           if(book == null)
                return BadRequest();

            var result= await _httpClient.PostAsJsonAsync<Book>($"api/v1/books/",book);
            var books = await result.Content.ReadFromJsonAsync<List<Book>>();

            if(books == null)
                return NotFound();
            return Ok(books);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (book == null || id ==0)
                return BadRequest();

            var result = await _httpClient.PutAsJsonAsync<Book>($"api/v1/books/{id}", book);
            var bookUp = await result.Content.ReadFromJsonAsync<Book>();

            if (bookUp == null)
                return NotFound();

            return Ok(bookUp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            if(id == 0)
                return BadRequest();

           return Ok( await _httpClient.DeleteAsync($"api/v1/books/{id}"));

        }
        
    }
}
