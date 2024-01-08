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
    public class EquipmentTypeManager
    {
        private readonly OpsRep<EquipmentType> equipmentTypeRepository;
        private readonly ILogger<EquipmentTypeManager> logger;
        

        public EquipmentTypeManager(
            OpsRep<EquipmentType> equipmentTypeRepository,
            ILogger<EquipmentTypeManager> logger)
        {
            this.equipmentTypeRepository = equipmentTypeRepository;
            this.logger = logger;
           
        }

        public async Task<IEnumerable<EquipmentType>> getEquipmentType(string title = null)
        {
            try
            {
               
                if (string.IsNullOrEmpty(title))
                {
                    throw new ArgumentException("Title cannot be empty.");
                }

                var query = await equipmentTypeRepository.Get(title != null ? x => x.Title == title : null);
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve equipment type with title : ", title);
                throw;
            }
        }
        public async Task<IEnumerable<EquipmentType>> GetAllEquipmentTypes()
        {
            try
            {
               
                return await equipmentTypeRepository.GetAll<EquipmentType>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve equipment types");
                throw;
            }
        }

        public async Task AddEquipmentType(EquipmentType equipmentType)
        {
            try
            {
                await equipmentTypeRepository.Add(equipmentType);
                await equipmentTypeRepository.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add equipment type");
                throw;
            }
        }

        public async Task UpdateEquipmentType(EquipmentType equipmentType)
        {
            try
            {
                await equipmentTypeRepository.Update(equipmentType);
                await equipmentTypeRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update equipment type");
                throw;
            }
        }

        public async Task RemoveEquipmentType(Guid equipmentTypeId)
        {
            try
            {
                var equipmentType =  await equipmentTypeRepository.FindById(equipmentTypeId);
                if (equipmentType == null)
                {
                    throw new InvalidOperationException("Equipment type not found");
                }

                await equipmentTypeRepository.Delete(equipmentType);
                await equipmentTypeRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove equipment type");
                throw;
            }
        }
    }
}
