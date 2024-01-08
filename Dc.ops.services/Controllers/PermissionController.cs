using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Routing.Constraints;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly UserManager permissionManager;
        public PermissionController(UserManager permissionManager)
        {
            this.permissionManager = permissionManager;
        }
        [HttpGet("GetAllPerm")]
        public async Task<ActionResult> GetAllPermission()
        {
            try
            {
                return Ok(await permissionManager.GetAllPermissions());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve Permissions");
            }
        }
        [HttpGet("GetPermissionById")]
        public async Task<ActionResult> GetPermissionsById(Guid id)
        {
            try
            {
                return Ok(await permissionManager.GetPermission(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve Permission by Id");
            }
        }
        [HttpPost("AddPermission")]
        public async Task<ActionResult> AddPermission ([FromQuery]Permission permission)
        {
            try
            {
                

                await permissionManager.AddPermission(permission);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to Add new permission");
            }
        }
        [HttpGet("GetPermissionByRole")]
        public async Task<ActionResult> GetPermissionByRole (Guid id)
        {
            try
            {
                return Ok(await permissionManager.GetPermissionByRole(id));
            }catch(Exception ex)
            {
                return StatusCode(500, "Failed to retrieve Permission by Role");
            }
        }
        [HttpGet("GetPermissionByUser")]
        public async Task<ActionResult> GetPermissionByUser(Guid userId)
        {
            try
            {
                return Ok(await permissionManager.GetPermissionByUser(userId));
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve Permission by User");
            }
        }
        [HttpPut("PermissionUpdate")]
        public async Task<ActionResult> UpdatePermission(Permission newpermission)
        {
            try
            {
                await permissionManager.UpdatePermission(newpermission);    
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, "Failed to Update Permission");
            }
        }
        [HttpDelete("RemovePermission")]
        public async Task<ActionResult> RemovePermission(Guid id)
        {
            try
            {
                await permissionManager.RemovePermission(id);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to remove Permission");
            }
        }
        [HttpPut("AssignPermission")]
        public async Task<ActionResult> AssignPermission(Guid permissionid, Guid roleid)
        {
            try
            {
                await permissionManager.AssignPermissionToRole(permissionid, roleid);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, "Failed to assign Permission");
            }
        }
        [HttpPut("UnAssignPermission")]
        public async Task<ActionResult> UnAssignPermission(Guid permissionId, Guid RoleId)
        {
            try{
                await permissionManager.UnassignPermissionFromRole(permissionId, RoleId);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to UnAssign Permission");
            }
        }
    }
}
