using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.Extensions.Logging;

namespace Dc.ops.services.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class EquipmentController : ControllerBase 
    {
        private readonly EquipmentManager equipmentManager;
       
        public EquipmentController(EquipmentManager equipmentManager) 
        {
            this.equipmentManager = equipmentManager;
            
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                
                return Ok(await equipmentManager.GetAllEquipment());
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Failed to retrieve equipment");
            }
        }
        [HttpPost("AddEquipment")]
        public async Task<ActionResult> AddEquipment([FromQuery] Equipment equipment)
        {
            try
            {
                await equipmentManager.AddEquipment(equipment);
                return Ok(); // Or return the created equipment details if needed
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Failed to add equipment");
            }
        }
        [HttpPut("UpdateEquipment")]
        public async Task<ActionResult> UpdateEquipment(Guid equipmentId, [FromQuery] Equipment equipment)
        {
            try
            {
                await equipmentManager.UpdateEquipment(equipment);
                return Ok(); // Or return the updated equipment details if needed
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Failed to update equipment");
            }
        }
        [HttpDelete("RemoveEquipment")]
        public async Task<ActionResult> RemoveEquipment(Guid equipmentId)
        {
            try
            {
                await equipmentManager.RemoveEquipmentAsync(equipmentId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Failed to remove equipment");
            }
        }
        //[HttpGet]
        //public async Task<ActionResult> GetEquipment([FromQuery] string title = null, [FromQuery] EquipmentType equipmentType = null)
        //{
        //    try
        //    {
        //        var equipmentList = await equipmentManager.GetEquipment(title, equipmentType);
        //        return Ok(equipmentList);
        //    }
        //    catch (Exception ex)
        //    {
        //       
        //        return StatusCode(500, "Failed to retrieve equipment");
        //    }
        //}
        [HttpGet("GetEquipmentById")]
        public async Task<ActionResult> GetEquipmentById(Guid equipmentId)
        {
            try
            {
                var equipment = await equipmentManager.GetEquipmentByIdAsync(equipmentId);

                // Create a list containing the equipment (if found) or an empty list
                List<Equipment> equipmentList = equipment != null ? new List<Equipment> { equipment } : new List<Equipment>();

                return Ok(equipmentList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve equipment");
            }
        }

    }
}
