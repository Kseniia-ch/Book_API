using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services;
using BookAPI.Extension;
using BookAPI.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly IBookService bookService; 
        private readonly IMapper mapper;
        public BooksController(IBookService bookService, IMapper mapper)
        {
            this.bookService = bookService;
            this.mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var books = await bookService.ListAsync();
            var resource = mapper.Map<IEnumerable<Book>,IEnumerable<BookResource>>(books);
            var result = new ResponseData
            {
                Data = resource,
                Success = true,
                Message = ""
            };
            return result;
        }
    
        [Authorize(Roles = "Saler")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveBookResource resource)
        {
            if (string.IsNullOrEmpty(resource.Publisher) && !string.IsNullOrEmpty(resource.City))
            {
                ModelState.AddModelError("Publisher", "The Publisher field is required.");
            }

            if (string.IsNullOrEmpty(resource.City) && !string.IsNullOrEmpty(resource.Publisher))
            {
                ModelState.AddModelError("City", "The City field is required.");
            }

            if(resource.Authors != null)
            {
                foreach (var author in resource.Authors)
                {
                    if (string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
                    {
                        ModelState.AddModelError("Author", "The Author field is required.");
                    }
                }
            }

           if (!ModelState.IsValid)
            {
                var badresult = new ResponseData
                {
                    Data = null,
                    Message = string.Join(" ", ModelState.GetErrorMessages().ToArray()),
                    Success = false
                };   
                return Ok(badresult);
            }

            var book = mapper.Map<SaveBookResource, Book>(resource);
            var bookResponse = await bookService.SaveAsync(book);  
            var bookResource = mapper.Map<Book, BookResource>(bookResponse.Book);
            var result = new ResponseData
            {
                Data = bookResource,
                Message = bookResponse.Message,
                Success = bookResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBookResource resource)
        {
            if (string.IsNullOrEmpty(resource.Publisher) && !string.IsNullOrEmpty(resource.City))
            {
                ModelState.AddModelError("Publisher", "The Publisher field is required.");
            }

            if (string.IsNullOrEmpty(resource.City) && !string.IsNullOrEmpty(resource.Publisher))
            {
                ModelState.AddModelError("City", "The City field is required.");
            }

            if(resource.Authors != null)
            {
                foreach (var author in resource.Authors)
                {
                    if (string.IsNullOrEmpty(author.FirstName) || string.IsNullOrEmpty(author.LastName))
                    {
                        ModelState.AddModelError("Author", "The Author field is required.");
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                var badresult = new ResponseData
                {
                    Data = null,
                    Message = string.Join(" ", ModelState.GetErrorMessages().ToArray()),
                    Success = false
                };   
                return Ok(badresult);
            }

            var book = mapper.Map<SaveBookResource, Book>(resource);
            var bookResponse = await bookService.UpdateAsync(id, book);            
            var bookResource = mapper.Map<Book, BookResource>(bookResponse.Book);

            var result = new ResponseData
                        {
                            Data = bookResource,
                            Message = bookResponse.Message,
                            Success = bookResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var bookResponse = await bookService.DeleteAsync(id);           
            var bookResource = mapper.Map<Book, BookResource>(bookResponse.Book);
            var result = new ResponseData
                        {
                            Data = bookResource,
                            Message = bookResponse.Message,
                            Success = bookResponse.Success
                        };            
            return Ok(result);
        }
    }
}