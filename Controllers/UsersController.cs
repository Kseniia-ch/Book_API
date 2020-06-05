using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services;
using BookAPI.Resources;
using BookAPI.Extension;
using Microsoft.AspNetCore.Authorization;

namespace BookAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        public UsersController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }


        [HttpGet]
        public async Task<ResponseData> GetAllAsync()
        {
            var users = await userService.ListAsync();
            var resource = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            var result = new ResponseData
            {
                Data = resource,
                Message = "",
                Success = true
            };
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUserResource resource)
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

            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.SaveAsync(user);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);

            var result = new ResponseData
                        {
                            Data = userResource,
                            Message = userResponse.Message,
                            Success = userResponse.Success
                        };            
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUserResource resource)
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

            var user = mapper.Map<SaveUserResource, User>(resource);
            var userResponse = await userService.UpdateAsync(id, user);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = new ResponseData
                        {
                            Data = userResource,
                            Message = userResponse.Message,
                            Success = userResponse.Success
                        };            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var userResponse = await userService.DeleteAsync(id);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = new ResponseData
                        {
                            Data = userResource,
                            Message = userResponse.Message,
                            Success = userResponse.Success
                        };            
            return Ok(result);
        }
    }
}