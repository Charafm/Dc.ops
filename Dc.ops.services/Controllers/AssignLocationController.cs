using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignLocationController : ControllerBase
    {
        private readonly AssignLocationManager locationManager;
        public AssignLocationController(AssignLocationManager locationManager)
        {
            this.locationManager = locationManager;
        }
        [HttpGet("GetAllLocations")]
        public async Task<ActionResult> GetAllLocations()
        {
            try
            {
                return Ok(await locationManager.GetAllLocationsAsync());
            }catch (Exception ex) { 
            return StatusCode (500, "Failed to retrieve all Locations");
            }
        }
        [HttpPost("AddLocation")]
        public async Task<ActionResult> AddAssignLocation(AssignLocation location)
        {
            try
            {
                await locationManager.AddLocationAsync(location);
                return Ok();

            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to Add new Assign location");
            }

        }
        [HttpPut("UpdateLocation")]
        public async Task<ActionResult> UpdateAssignLocation(AssignLocation newlocation)
        {
            try
            {
                await locationManager.UpdateAssignLocationAsync(newlocation);
                return Ok();    
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to Update Assign Location");
            }
        }
        [HttpDelete("RemoveLocation")]
        public async Task<ActionResult> RemoveAssignLocation (Guid id)
        {
            try
            {
                await locationManager.RemoveLocationAsync(id);
                return Ok();
            }catch(Exception ex)
            {
                return StatusCode(500, "Failed to remove AssignLocation");
            }
        }

    }
}
