using Dc.ops.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    public class EquipmentManager
    {
        private readonly OpsRep<Equipment> equipmentRepository;
        
        private readonly ILogger<EquipmentManager> logger;
        private readonly UserManager usermanager;
        private readonly EquipmentHistoryManager equipmentHistoryManager;

        public EquipmentManager(
            OpsRep<Equipment> equipmentRepository,
            ILogger<EquipmentManager> logger,
            UserManager usermanager,
            EquipmentHistoryManager equipmentHistoryManager)
            
        {
            this.equipmentRepository = equipmentRepository;
            
            this.logger = logger;
            this.usermanager = usermanager;
            this.equipmentHistoryManager = equipmentHistoryManager;
          
        }
        
        public async Task AddEquipment(Equipment equipment)
        {
            try
            {

                await equipmentRepository.Add(equipment);
                await equipmentRepository.SaveChangesAsync();

                
                await equipmentHistoryManager.LogEquipmentHistory(equipment, "Added");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding equipment");
                throw;
            }
        }

        public async Task UpdateEquipment(Equipment equipment)
        {
            try
            {
               

               await equipmentRepository.Update(equipment);
               await  equipmentRepository.SaveChangesAsync();

               await equipmentHistoryManager.LogEquipmentHistory(equipment, "Updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating equipment");
                throw;
            }
        }

        public async Task RemoveEquipmentAsync(Guid equipmentId)
        {
            try
            {
                var equipment =  await equipmentRepository.FindById(equipmentId);
                if (equipment == null)
                {
                    throw new InvalidOperationException("Equipment not found");
                }

                await equipmentRepository.Delete(equipment);
               await  equipmentRepository.SaveChangesAsync();

                await equipmentHistoryManager.LogEquipmentHistory(equipment, "Removed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while removing equipment");
                throw;
            }
        }

        public async Task<IEnumerable<Equipment>> GetAllEquipment()
        {
            try {

                return  await equipmentRepository.GetAll<Equipment>();
            } catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while getting all equipments");
                throw;
            }
        }
        public async Task<IEnumerable<Equipment>> GetEquipmentAsync(string title = null, EquipmentType equipmentType = null)
        {
            var query = equipmentRepository.Get(title != null ? x => x.Title == title : null);
            query = equipmentType != null ? query.Where(x => x.Type == equipmentType) : query;
            return await query.ToListAsync();
        }

        public async Task<Equipment> GetEquipmentByIdAsync(Guid equipmentId)
        {
            return  await equipmentRepository.FindById(equipmentId);
        }

      
        #region Dispose if needed
        public void Dispose()
        {
            // Dispose of resources, if necessary
        }
        #endregion
    }
}
