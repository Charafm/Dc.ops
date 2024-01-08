using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager roleManager;
        public RoleController(UserManager roleManager) { 
        this.roleManager = roleManager;
        }
        [HttpGet("GetAllRoles")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                return Ok(await roleManager.GetAllRoles()); 
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetRoleByID")]
        public async Task<ActionResult> GetRoleByiD (Guid id)
        {
            try
            {
                return Ok(await roleManager.GetRole(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetRoleByPermission")]
        public async Task<ActionResult> GetRoleByPer(Guid id)
        {
            try
            {
                return Ok(await roleManager.GetRoleByPermission(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetRoleByUser")]
        public async Task<ActionResult> getRoleByUser(Guid id)
        {
            try
            {
                return Ok(await roleManager.GetRoleByUser(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddRole")]
        public async Task<ActionResult> AddRole(UserRole role)
        {
            try
            {
                await roleManager.AddRole(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UpdateRole")]
        public async Task<ActionResult> UpdateRole(UserRole role)
        {
            try
            {
                await roleManager.UpdateRole(role);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("RemoveRole")]
        public async Task<ActionResult> RemoveRole(Guid id)
        {
            try
            {
                await roleManager.RemoveRole(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("AssignRole")]
        public async Task<ActionResult> AssignRole(Guid roleId, Guid userId)
        {
            try
            {
                await roleManager.AssignRoleToUser(roleId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("UnAssignRole")]
        public async Task<ActionResult> UnAssignRoleFromUser(Guid roleId, Guid userId)
        {
            try
            {
                await roleManager.UnassignRoleFromUser(roleId,userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
