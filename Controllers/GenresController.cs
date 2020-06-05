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
    public class GenresController: Controller
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;
        public GenresController(IGenreService genreService, IMapper mapper)
        {
            this.genreService = genreService;
            this.mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var genres = await genreService.ListAsync();
            var resource = mapper.Map<IEnumerable<Genre>,IEnumerable<GenreResource>>(genres);
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
        public async Task<IActionResult> PostAsync([FromBody] SaveGenreResource resource)
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

            var genre = mapper.Map<SaveGenreResource, Genre>(resource);
            var genreResponse = await genreService.SaveAsync(genre);  
            var genreResource = mapper.Map<Genre, GenreResource>(genreResponse.Genre);
            var result = new ResponseData
            {
                Data = genreResource,
                Message = genreResponse.Message,
                Success = genreResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveGenreResource resource)
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

            var genre = mapper.Map<SaveGenreResource, Genre>(resource);
            var genreResponse = await genreService.UpdateAsync(id, genre);            
            var genreResource = mapper.Map<Genre, GenreResource>(genreResponse.Genre);

            var result = new ResponseData
                        {
                            Data = genreResource,
                            Message = genreResponse.Message,
                            Success = genreResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var genreResponse = await genreService.DeleteAsync(id);           
            var genreResource = mapper.Map<Genre, GenreResource>(genreResponse.Genre);
            var result = new ResponseData
                        {
                            Data = genreResource,
                            Message = genreResponse.Message,
                            Success = genreResponse.Success
                        };            
            return Ok(result);
        }
    }
}