using Dc.ops.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Manager.Managers
{
    public class AssignLocationManager
    {
        private readonly OpsRep<AssignLocation> locationRepository;
        private readonly ILogger<AssignLocationManager> logger;
      
       

        public AssignLocationManager(
            OpsRep<AssignLocation> locationRepository,
            ILogger<AssignLocationManager> logger,
            EquipmentHistoryManager equipmentHistoryManager)
        {
            this.locationRepository = locationRepository;
            this.logger = logger;
          
        }

        public async Task<IEnumerable<AssignLocation>> GetAllLocationsAsync()
        {
            try
            {
                // Adjust based on your repository's method for retrieving locations
                return  await locationRepository.GetAll<AssignLocation>();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to retrieve locations");
                throw;
            }
        }

        public async Task AddLocationAsync(AssignLocation location)
        {
            try
            {
                await locationRepository.Add(location);
                await locationRepository.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to add location");
                throw;
            }
        }

        public async Task UpdateAssignLocationAsync(AssignLocation location)
        {
            try
            {
               await locationRepository.Update(location);
                await locationRepository.SaveChangesAsync();
               
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update assigned location");
                throw;
            }
        }

        public async Task RemoveLocationAsync(Guid locationId)
        {
            try
            {
                var location = await GetLocationById(locationId);  
                if (location == null)
                {
                    throw new InvalidOperationException("Location not found");
                }

                // this should be linked to the assign history if the location has an equipment assigned to it
                //if (location.Equipment.Any())
                //{
                //    throw new InvalidOperationException("Cannot remove a location that is currently assigned to equipment");
                //}

                await locationRepository.Delete(location);
                await locationRepository.SaveChangesAsync();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to remove location");
                throw;
            }
        }
        public async Task<IEnumerable<AssignLocation>> GetLocationById(Guid locationId)
        {
            try
            {

                var locations = await locationRepository.Get(l => l.Id == locationId);

                if (!locations.Any())
                {
                    logger.LogWarning("No locations found with ID {LocationId}", locationId);
                    return Enumerable.Empty<AssignLocation>(); // Return an empty collection
                }

                return locations;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error getting location");
                throw;
            }
        }
    }
}
