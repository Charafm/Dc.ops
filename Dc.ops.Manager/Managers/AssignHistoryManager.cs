using Dc.ops.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    public class AssignHistoryManager
    {
        private readonly OpsRep<AssignHistory> repository;
        private readonly UserManager userManager;

        public AssignHistoryManager(OpsRep<AssignHistory> repository, UserManager userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        // AssignEquipment
        public async Task AssignEquipment(Equipment equipment, AssignLocation assignLocation)
        {
            // Update equipment assignment in the database
            // ... (logic to assign equipment to location)

            // Log assign history
            await LogAssignHistory(equipment, assignLocation);
        }

        // LogAssignHistory
        public async Task LogAssignHistory(Equipment equipment, AssignLocation assignLocation)
        {
            var assignHistory = new AssignHistory
            {
                Equipment = equipment,
                AssignLocation = assignLocation, 
                Date = DateTime.Now,
                User =  userManager.GetCurrentUser() 
            };
            await repository.Add(assignHistory);
            await repository.SaveChangesAsync();
        }

        // GetAllAssignHistory
        public async Task<IEnumerable<AssignHistory>> GetAllAssignHistory()
        {
            return  await repository.GetAll<AssignHistory>();
        }

        // GetAssignHistory based on various criteria
        public async Task<IEnumerable<AssignHistory>> GetAssignHistory(
            Guid? equipmentId = null,
            Guid? assignLocationId = null,
            Guid? userId = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            // Build a query with optional filters
            var query = await repository.GetAll<AssignHistory>();

            if (equipmentId.HasValue)
            {
                query = query.Where(ah => ah.Equipment.Id == equipmentId.Value);
            }

            if (assignLocationId.HasValue)
            {
                query = query.Where(ah => ah.AssignLocation.Id == assignLocationId.Value);
            }

            if (userId.HasValue)
            {
                query = query.Where(ah => ah.User.Id == userId.Value);
            }

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(ah => ah.Date >= startDate.Value && ah.Date <= endDate.Value);
            }

            return  query.ToList();
        }

    }
}
