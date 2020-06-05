using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BookAPI.Domain.Models;
using BookAPI.Domain.Services;
using BookAPI.Resources;
using Microsoft.AspNetCore.Authorization;

namespace BookAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    public class UserRolesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUserRolesService userRoleService;
        public UserRolesController(IMapper mapper, IUserRolesService userRoleService)
        {
            this.mapper = mapper;
            this.userRoleService = userRoleService;
        }
        [HttpGet("{id}")]
        public async Task<ResponseData> ListUsersByRoleAsync(int id) 
        {
            var users = await userRoleService.ListUsersByRoleAsync(id);
            var userResource = mapper.Map<IEnumerable<User>, IEnumerable<UserResource>>(users);
            var result = new ResponseData
            {
                Data = userResource,
                Success = true,
                Message = ""
            };
            return result;
        }

        [HttpPost]
        public async Task<ResponseData> SetUserRole([FromBody] SaveUserRoleResource resource)
        {
            var userResponse = await userRoleService.SetRole(resource.UserId, resource.RoleId);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = new ResponseData
            {
                Success = true,
                Message = "",
                Data = userResource
            };
            return result;
        }

        [HttpDelete]
        public async Task<ResponseData> DeleteUserRole([FromBody] SaveUserRoleResource resource)
        {
            var userResponse = await userRoleService.DeleteRole(resource.UserId, resource.RoleId);
            var userResource = mapper.Map<User, UserResource>(userResponse.User);
            var result = new ResponseData
            {
                Success = true,
                Message = "",
                Data = userResource
            };
            return result;
        }
    }
}