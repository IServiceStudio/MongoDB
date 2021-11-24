using MongoAPI.Models;
using MongoDB.Driver;

namespace MongoAPI.Service
{
    public class BookService
    {
        #region 成员变量及其属性

        private readonly IMongoCollection<Book> _books;

        #endregion

        #region 构造函数

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        #endregion

        #region 公共函数

        public List<Book> Get() => _books.Find(book => true).ToList();

        public Book Get(string id) => _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) => _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(string id) => _books.DeleteOne(book => book.Id == id);

        #endregion

        #region 私有函数

        #endregion
    }
}
