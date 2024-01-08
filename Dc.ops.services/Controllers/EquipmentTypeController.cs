using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentTypeController : ControllerBase
    {
        private readonly EquipmentTypeManager equipmentTypeManager;

        public EquipmentTypeController(EquipmentTypeManager equipmentTypeManager)
        {
            this.equipmentTypeManager = equipmentTypeManager;
        }
        //[HttpGet]
        //public async Task<ActionResult> GetEquipmentType([FromQuery] string title = null)
        //{
        //    try
        //    {
        //        var equipmentTypes = await equipmentTypeManager.getEquipmentType(title);
        //        return Ok(equipmentTypes);
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //       
        //        return StatusCode(500, "Failed to retrieve equipment types");
        //    }
        //}
        [HttpGet("GetAllEquipmentTypes")]
        public async Task<ActionResult> GetAllEquipmentTypes()
        {
            try
            {
                var equipmentTypes = await equipmentTypeManager.GetAllEquipmentTypes();
                return Ok(equipmentTypes);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Failed to retrieve equipment types");
            }
        }
        [HttpPost("AddEquipmentType")]
        public async Task<ActionResult> AddEquipmentType([FromQuery] EquipmentType equipmentType)
        {
            try
            {
                await equipmentTypeManager.AddEquipmentType(equipmentType);
                return Ok(); // Or return the created equipment type details if needed
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, "Failed to add equipment type");
            }
        }
        [HttpPut("UpdateEquipmentType")]
        public async Task<ActionResult> UpdateEquipmentType([FromBody] EquipmentType equipmentType)
        {
            try
            {
                await equipmentTypeManager.UpdateEquipmentType(equipmentType);
                return Ok(); // Or return the updated equipment type details if needed
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, "Failed to update equipment type");
            }
        }
        [HttpDelete("RemoveEquipmentType")]
        public async Task<ActionResult> RemoveEquipmentType(Guid equipmentTypeId)
        {
            try
            {
                await equipmentTypeManager.RemoveEquipmentType(equipmentTypeId);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, "Failed to remove equipment type");
            }
        }

    }

}
