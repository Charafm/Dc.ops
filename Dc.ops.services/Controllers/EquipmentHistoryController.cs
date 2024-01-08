using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentHistoryController : ControllerBase
    {
        private readonly EquipmentHistoryManager historyManager;
        public EquipmentHistoryController(EquipmentHistoryManager historyManager)
        {
            this.historyManager = historyManager;
        }
        [HttpGet("AllEquipmentHistory")]
        public async Task<ActionResult> GetAllEquipmentHistory()
        {
            try
            {
                return Ok(await historyManager.GetAllEquipmentHistories());
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed retrieving all equipment history");
            }
             
        }
        [HttpGet("EquipmentHistorySearch")]
        public async Task<ActionResult> EquipmentHistorySearch(Guid? equipmentId = null,
            Guid? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? actionType = null)
        {
            try
            {
                
                return Ok(await historyManager.GetEquipmentHistory(equipmentId, userId, startDate, endDate, actionType));
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve specific equipment History");
            }
        }
        [HttpPost("LogEquipmentChanges")]
        public async Task<ActionResult> LogEquipmentChanges(Equipment equipment, string action, string? notes = null)
        {
            try
            {
                await historyManager.LogEquipmentHistory(equipment, action, notes);
                return Ok();
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed logging equipment changes");
            }
        }
        [HttpDelete("RemoveEquipmentHistory")]
        public async Task<ActionResult> RemoveEquipmentHistory(Guid historyId)
        {
            try
            {
                await historyManager.RemoveEquipmentHistory(historyId);
                return Ok();
            }catch(Exception ex) {
                return StatusCode(500, "Failed to Remove Equipment History");
            }
        }
    }
}
