using Booksv2.Shared;
using System.Net.Http.Json;

namespace Booksv2.Client.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _httpClientLocal;
        public List<Book> books = new List<Book>();
        public BookService(HttpClient httpClient)
        {
            _httpClientLocal = httpClient;
        }

        public async Task<Book> CreateBook(Book book)
        {
            var books = await _httpClientLocal.PostAsJsonAsync($"api/v1/Books/", book);
            if (books.IsSuccessStatusCode)
            {
                var result = await books.Content.ReadFromJsonAsync<Book>();

            }
            else
            {
                return null;
            }
            return book;
        }

        public async Task<HttpResponseMessage> DeleteBook(int id)
        {
            return await _httpClientLocal.DeleteAsync($"api/Book/{id}");
        }

        public async Task<Book> GetBook(int id)
        {
            return await _httpClientLocal.GetFromJsonAsync<Book>($"api/Book/{id}");
            
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _httpClientLocal.GetFromJsonAsync<List<Book>>("api/Book");

        }

        public async Task<HttpResponseMessage> UpdateBook(Book book)
        {
            return await _httpClientLocal.PutAsJsonAsync<Book>($"api/Book/{book.Id}", book);

        }
    }
}

