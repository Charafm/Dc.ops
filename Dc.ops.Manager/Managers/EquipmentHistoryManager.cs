using Dc.ops.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    public class EquipmentHistoryManager
    {
        private readonly OpsRep<EquipmentHistory> repository;
        private readonly UserManager userManager;

        public EquipmentHistoryManager(OpsRep<EquipmentHistory> repository, UserManager userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        // LogEquipmentHistory (already implemented)
        public async Task LogEquipmentHistory(Equipment equipment, string action, string notes = null)
        {
            var equipmentHistory = new EquipmentHistory
            {
                Equipment = equipment,
                Action = action,
                Notes = notes,
                Date = DateTime.UtcNow,
                User = userManager.GetCurrentUser() // Get current user
            };
            await repository.Add(equipmentHistory);
            await repository.SaveChangesAsync();
        }

        // GetAllEquipmentHistories
        public async Task<IEnumerable<EquipmentHistory>> GetAllEquipmentHistories()
        {
            return  await repository.GetAll<EquipmentHistory>();
        }

        // GetEquipmentHistory based on various criteria
        public async Task<IEnumerable<EquipmentHistory>> GetEquipmentHistory(
            Guid? equipmentId = null,
            Guid? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? actionType = null)
        {
            // Build a query with optional filters
            var query = await repository.GetAll<EquipmentHistory>();

            if (equipmentId.HasValue)
            {
                query = query.Where(eh => eh.Equipment.Id == equipmentId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(eh => eh.User.Id == userId.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(eh => eh.Date >= startDate.Value && eh.Date <= endDate.Value);
            }

            if (!string.IsNullOrEmpty(actionType))
            {
                query = query.Where(eh => eh.Action == actionType);
            }

            return query.ToList();
        }

        // RemoveEquipmentHistory
        public async Task RemoveEquipmentHistory(Guid historyId)
        {
            var equipmentHistory = repository.FindById(historyId);
            if (equipmentHistory != null)
            {
                await repository.Delete(equipmentHistory);
                await repository.SaveChangesAsync();
            }
        }
    }
}
