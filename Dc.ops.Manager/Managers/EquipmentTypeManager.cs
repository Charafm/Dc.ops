using Dc.ops.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    internal class EquipmentTypeManager
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

        public async Task<IEnumerable<EquipmentType>> GetAllEquipmentTypesAsync()
        {
            try
            {
               
                return equipmentTypeRepository.GetAll<EquipmentType>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve equipment types");
                throw;
            }
        }

        public async Task AddEquipmentTypeAsync(EquipmentType equipmentType)
        {
            try
            {
                 equipmentTypeRepository.Add(equipmentType);
                await equipmentTypeRepository.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add equipment type");
                throw;
            }
        }

        public async Task UpdateEquipmentTypeAsync(EquipmentType equipmentType)
        {
            try
            {
                equipmentTypeRepository.Update(equipmentType);
                await equipmentTypeRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update equipment type");
                throw;
            }
        }

        public async Task RemoveEquipmentTypeAsync(int equipmentTypeId)
        {
            try
            {
                var equipmentType =  equipmentTypeRepository.FindById(equipmentTypeId);
                if (equipmentType == null)
                {
                    throw new InvalidOperationException("Equipment type not found");
                }

                equipmentTypeRepository.Delete(equipmentType);
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
