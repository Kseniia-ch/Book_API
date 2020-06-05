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
    [Route("api/[controller]")]
    public class RolesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IRoleService roleService;
        public RolesController(IMapper mapper, IRoleService roleService)
        {
            this.mapper = mapper;
            this.roleService = roleService;
        }
        [HttpGet]
        public async Task<ResponseData> GetAllAsync()
        {
            var roles = await roleService.ListAsync();
            var resource = mapper.Map<IEnumerable<Role>, IEnumerable<RoleResource>>(roles);
            var result = new ResponseData
            {
                Data = resource,
                Message = "",
                Success = true
            };
            return result;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveRoleResource resource)
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
            
            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.SaveAsync(role);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = new ResponseData
            {
                Data = roleResource,
                Success = roleResponse.Success,
                Message = roleResponse.Message
            };
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRoleResource resource)
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

            var role = mapper.Map<SaveRoleResource, Role>(resource);
            var roleResponse = await roleService.UpdateAsync(id, role);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
             var result = new ResponseData
            {
                Data = roleResource,
                Success = roleResponse.Success,
                Message = roleResponse.Message
            };
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var roleResponse = await roleService.DeleteAsync(id);
            var roleResource = mapper.Map<Role, RoleResource>(roleResponse.Role);
            var result = new ResponseData
            {
                Data = roleResource,
                Success = roleResponse.Success,
                Message = roleResponse.Message
            };
            return Ok(result);
        }
    }
}