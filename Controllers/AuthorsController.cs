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
    public class AuthorsController: Controller
    {
        private readonly IAuthorService authorService;
        private readonly IMapper mapper;
        public AuthorsController(IAuthorService authorService, IMapper mapper)
        {
            this.authorService = authorService;
            this.mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var authors = await authorService.ListAsync();
            var resource = mapper.Map<IEnumerable<Author>,IEnumerable<AuthorResource>>(authors);
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
        public async Task<IActionResult> PostAsync([FromBody] SaveAuthorResource resource)
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

            var author = mapper.Map<SaveAuthorResource, Author>(resource);
            var authorResponse = await authorService.SaveAsync(author);  
            var authorResource = mapper.Map<Author, AuthorResource>(authorResponse.Author);
            var result = new ResponseData
            {
                Data = authorResource,
                Message = authorResponse.Message,
                Success = authorResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAuthorResource resource)
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

            var author = mapper.Map<SaveAuthorResource, Author>(resource);
            var authorResponse = await authorService.UpdateAsync(id, author);            
            var authorResource = mapper.Map<Author, AuthorResource>(authorResponse.Author);

            var result = new ResponseData
                        {
                            Data = authorResource,
                            Message = authorResponse.Message,
                            Success = authorResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var authorResponse = await authorService.DeleteAsync(id);           
            var authorResource = mapper.Map<Author, AuthorResource>(authorResponse.Author);
            var result = new ResponseData
                        {
                            Data = authorResource,
                            Message = authorResponse.Message,
                            Success = authorResponse.Success
                        };            
            return Ok(result);
        }
    }
}