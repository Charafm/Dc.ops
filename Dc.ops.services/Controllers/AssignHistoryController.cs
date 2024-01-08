using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dc.ops.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssignHistoryController : ControllerBase
    {
        private readonly AssignHistoryManager historyManager;
        public AssignHistoryController(AssignHistoryManager historyManager)
        {
            this.historyManager = historyManager;
        }
        [HttpGet("GetAllAssignHistory")]
        public async Task<ActionResult> GetAllAssignHistory()
        {
            try {
                return Ok(await historyManager.GetAllAssignHistory());
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed to retrieve all Assign History");
            }
        }
        //[HttpPost("AssignEquipment")]
        //public async Task<ActionResult> AssignEquipment(Equipment equipment, AssignLocation location)
        //{
        //    try
        //    {
        //        await historyManager.AssignEquipment(equipment, location);
        //        return Ok();
        //    }catch (Exception ex)
        //    {
        //        return StatusCode(500, "Failed to Assign Equipment to given Location");
        //    }
        //}
        [HttpGet("AssignHistorySearch")]
        public async Task<ActionResult> AssignHistorySearch(Guid? equipmentId = null,
            Guid? assignLocationId = null,
            Guid? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            try
            {

                return Ok(await historyManager.GetAssignHistory(equipmentId, assignLocationId, userId, startDate, endDate));
            }catch (Exception ex)
            {
                return StatusCode(500, "Failed retrieving desired assign history");
            }
        }
    }
}
