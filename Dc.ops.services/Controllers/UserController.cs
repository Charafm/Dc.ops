using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager userManager;
        public UserController(UserManager userManager) {
        this.userManager = userManager;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {

                return Ok(await userManager.GetAllUsersAsync());
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Failed to retrieve equipment");
            }
        }
        [HttpGet("GetUserByID")]
        public async Task<ActionResult> GetUserByID (Guid id)
        {
            try
            {
                return Ok(await userManager.GetUserByIdAsync(id));
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetCurrent")]
        public async Task<ActionResult> getCurrentUser()
        {
            try
            {
                return Ok( userManager.GetCurrentUser());
            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("AddUser")]
        public async Task<ActionResult> AddUser([FromQuery] User user)
        {
            try
            {
                await userManager.CreateUserAsync(user);
                return Ok("User Added!");
            }catch(Exception ex)
            {
                return StatusCode(500, "Failed to add User");
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<ActionResult> UpdateUser (User user)
        {
            try
            {
                await userManager.UpdateUserAsync(user);
                return Ok("User Updated!");
            }catch(Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("DeleteUser")]
        public async Task<ActionResult> DeleteUser (Guid id)
        {
            try
            {
                await userManager.DeleteUserAsync(id);
                return Ok("User Deleted");

            }catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}

