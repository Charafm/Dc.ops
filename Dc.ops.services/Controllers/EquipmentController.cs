using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dc.ops.Entities;
using Dc.ops.Manager.Managers;

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

        [HttpGet]
        public IActionResult GetAll() 
        {
            var equipment = equipmentManager.GetAllEquipment();
            return Ok(equipment); 
        }
    }
}
