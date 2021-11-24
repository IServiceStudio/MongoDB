using Microsoft.AspNetCore.Mvc;
using MongoAPI.Models;
using MongoAPI.Service;

namespace MongoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region 成员变量及其属性

        private readonly BookService _bookService;

        #endregion

        #region 构造函数

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        #endregion

        #region 公共函数

        [HttpGet]
        public List<Book> Get() => _bookService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public Book Get(string id) => _bookService.Get(id);

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);
            return CreatedAtRoute("GetBook", book.Id.ToString(), book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        { 
            var book = _bookService.Get(id);
            if(book==null) return NotFound();
            _bookService.Update(id,bookIn);
            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null) return NotFound();
            _bookService.Remove(id);
            return NoContent();
        }

        #endregion

        #region 私有函数

        #endregion
    }
}
