using Booksv2.Shared;

namespace Booksv2.Client.Services
{
    public interface IBookService
    {
        Task<Book?> CreateBook(Book book);
        Task<List<Book>?> GetBooks();
        Task<Book?> GetBook(int id);
        Task<HttpResponseMessage> UpdateBook(Book book);
        Task<HttpResponseMessage> DeleteBook(int id);

    }
}
