using System;
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
    [Authorize]
    [Route("api/[controller]")]
    public class BooksOrderedController: Controller
    {
        
        private readonly IBookOrderedService bookOrderedService;
        private readonly IMapper mapper;

        public BooksOrderedController(IBookOrderedService bookOrderedService, IMapper mapper)
        {
            this.bookOrderedService = bookOrderedService;
            this.mapper = mapper;
        }
       
        [Authorize(Roles = "User,Saler")]
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var bookOrdereds = await bookOrderedService.ListAsync();
            var resource = mapper.Map<IEnumerable<BookOrdered>,IEnumerable<BookOrderedResource>>(bookOrdereds);
            var result = new ResponseData
            {
                Data = resource,
                Success = true,
                Message = ""
            };
            return result;
        }
    
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveBookOrderedResource resource)
        {
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

            var bookOrdered = mapper.Map<SaveBookOrderedResource, BookOrdered>(resource);

            bookOrdered.UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
            bookOrdered.DateAdded = DateTime.Now;

            var bookOrderedResponse = await bookOrderedService.SaveAsync(bookOrdered);  
            var bookOrderedResource = mapper.Map<BookOrdered, BookOrderedResource>(bookOrderedResponse.BookOrdered);
            var result = new ResponseData
            {
                Data = bookOrderedResource,
                Message = bookOrderedResponse.Message,
                Success = bookOrderedResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "User,Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBookOrderedResource resource)
        {
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

            var bookOrdered = mapper.Map<SaveBookOrderedResource, BookOrdered>(resource);
            var bookOrderedResponse = await bookOrderedService.UpdateAsync(id, bookOrdered);            
            var bookOrderedResource = mapper.Map<BookOrdered, BookOrderedResource>(bookOrderedResponse.BookOrdered);

            var result = new ResponseData
                        {
                            Data = bookOrderedResource,
                            Message = bookOrderedResponse.Message,
                            Success = bookOrderedResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "User,Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var bookOrderedResponse = await bookOrderedService.DeleteAsync(id);           
            var bookOrderedResource = mapper.Map<BookOrdered, BookOrderedResource>(bookOrderedResponse.BookOrdered);
            var result = new ResponseData
                        {
                            Data = bookOrderedResource,
                            Message = bookOrderedResponse.Message,
                            Success = bookOrderedResponse.Success
                        };            
            return Ok(result);
        }
    }
}