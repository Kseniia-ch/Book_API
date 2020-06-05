using System;
using System.Collections.Generic;
using System.Security.Claims;
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
    public class BooksForSaleController: Controller
    {
        
        private readonly IBookForSaleService bookForSaleService;
        private readonly IMapper mapper;

        public BooksForSaleController(IBookForSaleService bookForSaleService, IMapper mapper)
        {
            this.bookForSaleService = bookForSaleService;
            this.mapper = mapper;
        }

        [Authorize(Roles = "User,Saler")]
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var bookForSales = await bookForSaleService.ListAsync();
            var resource = mapper.Map<IEnumerable<BookForSale>,IEnumerable<BookForSaleResource>>(bookForSales);
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
        public async Task<IActionResult> PostAsync([FromBody] SaveBookForSaleResource resource)
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

            var bookForSale = mapper.Map<SaveBookForSaleResource, BookForSale>(resource);

            bookForSale.UserId = Convert.ToInt32(User.FindFirst("UserId").Value);
            bookForSale.DateAdded = DateTime.Now;

            var bookForSaleResponse = await bookForSaleService.SaveAsync(bookForSale);  
            var bookForSaleResource = mapper.Map<BookForSale, BookForSaleResource>(bookForSaleResponse.BookForSale);
            var result = new ResponseData
            {
                Data = bookForSaleResource,
                Message = bookForSaleResponse.Message,
                Success = bookForSaleResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveBookForSaleResource resource)
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

            var bookForSale = mapper.Map<SaveBookForSaleResource, BookForSale>(resource);
            var bookForSaleResponse = await bookForSaleService.UpdateAsync(id, bookForSale);            
            var bookForSaleResource = mapper.Map<BookForSale, BookForSaleResource>(bookForSaleResponse.BookForSale);

            var result = new ResponseData
                        {
                            Data = bookForSaleResource,
                            Message = bookForSaleResponse.Message,
                            Success = bookForSaleResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var bookForSaleResponse = await bookForSaleService.DeleteAsync(id);           
            var bookForSaleResource = mapper.Map<BookForSale, BookForSaleResource>(bookForSaleResponse.BookForSale);
            var result = new ResponseData
                        {
                            Data = bookForSaleResource,
                            Message = bookForSaleResponse.Message,
                            Success = bookForSaleResponse.Success
                        };            
            return Ok(result);
        }
    }
}