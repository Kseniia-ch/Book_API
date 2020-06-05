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
    public class PublishersController: Controller
    {
        private readonly IPublisherService publisherService;
        private readonly IMapper mapper;
        public PublishersController(IPublisherService publisherService, IMapper mapper)
        {
            this.publisherService = publisherService;
            this.mapper = mapper;
        }
       
        [HttpGet]
        public async Task<ResponseData> GetAllAsync() 
        {
            var publishers = await publisherService.ListAsync();
            var resource = mapper.Map<IEnumerable<Publisher>,IEnumerable<PublisherResource>>(publishers);
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
        public async Task<IActionResult> PostAsync([FromBody] SavePublisherResource resource)
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

            var publisher = mapper.Map<SavePublisherResource, Publisher>(resource);
            var publisherResponse = await publisherService.SaveAsync(publisher);  
            var publisherResource = mapper.Map<Publisher, PublisherResource>(publisherResponse.Publisher);
            var result = new ResponseData
            {
                Data = publisherResource,
                Message = publisherResponse.Message,
                Success = publisherResponse.Success
            };
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SavePublisherResource resource)
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

            var publisher = mapper.Map<SavePublisherResource, Publisher>(resource);
            var publisherResponse = await publisherService.UpdateAsync(id, publisher);            
            var publisherResource = mapper.Map<Publisher, PublisherResource>(publisherResponse.Publisher);

            var result = new ResponseData
                        {
                            Data = publisherResource,
                            Message = publisherResponse.Message,
                            Success = publisherResponse.Success
                        };            
            return Ok(result);
        }

        [Authorize(Roles = "Saler")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var publisherResponse = await publisherService.DeleteAsync(id);           
            var publisherResource = mapper.Map<Publisher, PublisherResource>(publisherResponse.Publisher);
            var result = new ResponseData
                        {
                            Data = publisherResource,
                            Message = publisherResponse.Message,
                            Success = publisherResponse.Success
                        };            
            return Ok(result);
        }
    }
}