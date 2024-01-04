using Dc.ops.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    internal class EquipmentManager
    {
        private readonly OpsRep<Equipment> equipmentRepository;
        private readonly OpsRep<EquipmentHistory> equipmentHistoryRepository;
        private readonly ILogger<EquipmentManager> logger;

        public EquipmentManager(
            OpsRep<Equipment> equipmentRepository,
            OpsRep<EquipmentHistory> equipmentHistoryRepository,
            ILogger<EquipmentManager> logger)
        {
            this.equipmentRepository = equipmentRepository;
            this.equipmentHistoryRepository = equipmentHistoryRepository;
            this.logger = logger;
        }

        public async Task AddEquipmentAsync(Equipment equipment)
        {
            try
            {
                equipmentRepository.Add(equipment);
                await equipmentRepository.SaveChangesAsync();

                
                await LogEquipmentHistoryAsync(equipment, "Added");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while adding equipment");
                throw;
            }
        }

        public async Task UpdateEquipmentAsync(Equipment equipment)
        {
            try
            {
                // Validate equipment properties here

                equipmentRepository.Update(equipment);
               await  equipmentRepository.SaveChangesAsync();

                await LogEquipmentHistoryAsync(equipment, "Updated");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while updating equipment");
                throw;
            }
        }

        public async Task RemoveEquipmentAsync(int equipmentId)
        {
            try
            {
                var equipment =  equipmentRepository.FindById(equipmentId);
                if (equipment == null)
                {
                    throw new InvalidOperationException("Equipment not found");
                }

                equipmentRepository.Delete(equipment);
               await  equipmentRepository.SaveChangesAsync();

                await LogEquipmentHistoryAsync(equipment, "Removed");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while removing equipment");
                throw;
            }
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync(string title = null, EquipmentType equipmentType = null)
        {
            var query = equipmentRepository.Get(title != null ? x => x.Title == title : null);
            query = equipmentType != null ? query.Where(x => x.Type == equipmentType) : query;
            return await query.ToListAsync();
        }

        public async Task<Equipment> GetEquipmentByIdAsync(int equipmentId)
        {
            return  equipmentRepository.FindById(equipmentId);
        }

        #region ActionLogging (NOT DONE)
        private async Task LogEquipmentHistoryAsync(Equipment equipment, string action)
        {
            var historyEntry = new EquipmentHistory
            {
                Equipment = equipment,
                Action = action,
                Date = DateTime.Now
                // Add other relevant details
                /*
                User = user,
                
                Notes = note;
                 */
            };
            equipmentHistoryRepository.Add(historyEntry);
            await equipmentHistoryRepository.SaveChangesAsync();
        }
        #endregion
        #region Dispose if needed
        public void Dispose()
        {
            // Dispose of resources, if necessary
        }
        #endregion
    }
}
