using Dc.ops.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace Dc.ops.Manager.Managers
{   
    public class UserManager
    {
        private readonly OpsRep<User> userRepository;
        private readonly OpsRep<UserRole> roleRepository;
        private readonly OpsRep<Permission> permissionRepository;
        private readonly ILogger<UserManager> logger;
        public UserManager(ILogger<UserManager> logger, OpsRep<User> userRepository, OpsRep<UserRole> roleRepository, OpsRep<Permission> permissionRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
            this.logger = logger;
        }
        #region User
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await userRepository.GetAll<User>();
            }
            catch (Exception ex) {
                logger.LogError(ex, "An error occurred while retrieving Users");
                throw;
            }
            
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            try
            {
                return await userRepository.FindById(id);
            }catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving Users");
                throw;
            }
        }


        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                //No hashing till everything is Done!
                await userRepository.Add(user);
                await userRepository.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An Error occurred while creating user");
                throw;
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            try
            {
                await userRepository.Update(user);
                await userRepository.SaveChangesAsync();
            }catch (Exception ex)
            {
                logger.LogError(ex, " An Error occurred while updating user");
                throw;
            }
           
        }

        public async Task DeleteUserAsync(Guid id)
        {
            try
            {
                await userRepository.Delete(id);
                await userRepository.SaveChangesAsync();
            }catch(Exception ex)
            {
                logger.LogError(ex, "An Error occurred when deleting user");
                throw;
            }
            
        }
        public User GetCurrentUser()
        {
            //var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if (userId != null)
            //{
            //    return userRepository.GetUserById(userId).Result; 
            //}
            //else
            //{
            //    return null; // No authenticated user
            //}

            // Temporary solution for testing and development:
            return new User { Username = "TemporaryUser" , PasswordHash = "TemporaryUser" }; // hard coded solution for early testing and development
        }
        #endregion

        #region Role
        public async Task<IEnumerable<UserRole>> GetAllRoles()
        {
            try
            {
                return await roleRepository.GetAll<UserRole>();
            }catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving roles");
                throw;
            }
           
        }

        public async Task<UserRole> GetRole(Guid roleId)
        {
            try
            {
                return await roleRepository.FindById(roleId);
            }catch (Exception ex)
            {
                logger.LogError(ex, "An Error occurred when retrieving the role");
                throw;
            }
           
        }

        public async Task<UserRole> GetRoleByPermission(Guid permissionId)
        {
            try
            {
                return (UserRole)await roleRepository.Get(
                r => r.Permissions.Any(p => p.Id == permissionId));
            }catch (Exception ex)
            {
                logger.LogError(ex, "An Error occurred when retrieving the role linked to given permission");
                throw;
            }
            
        }

        public async Task<UserRole> GetRoleByUser(Guid userId)
        {
            try
            {
                return (UserRole)await roleRepository.Get(
                r => r.Users.Any(u => u.Id == userId));

            }catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in GetRoleByUser");
                throw;
            }
            
        }

        public async Task AddRole(UserRole role)
        {
            try
            {
                await roleRepository.Add(role);
                await roleRepository.SaveChangesAsync();
            }catch (Exception ex) {
                logger.LogError(ex, "Error occurred in AddRole");
                throw;
            }
        }

        public async Task UpdateRole(UserRole role)
        {
            try
            {
                await roleRepository.Update(role);
                await roleRepository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in UpdateRole");
                throw;
            }
            
        }

        public async Task RemoveRole(Guid roleId)
        {
            try
            {
                var role = await GetRole(roleId);
                if (role != null)
                {
                    await roleRepository.Delete(role);
                    await roleRepository.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred in RemoveRole");
                throw;
            }
           
        }

        public async Task AssignRoleToUser(Guid roleId, Guid userId)
        {
            var user =  await userRepository.FindById(userId);
            
            var role = await GetRole(roleId);
            if (user != null && role != null)
            {
                user.Role = role;
                await userRepository.Update(user);
                await roleRepository.SaveChangesAsync();
            }
        }

        public async Task UnassignRoleFromUser(Guid roleId, Guid userId)
        {
            var user =  await userRepository.FindById(userId);
            var role = await GetRole(roleId);
            if (user != null && role != null)
            {
                if(user.Role != null && user.Role == role)
                {
                    user.Role = null;
                    await userRepository.Update(user);
                    await roleRepository.SaveChangesAsync();
                }
                
            }
        }

        #endregion

        #region Permission
        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            
            return  await permissionRepository.GetAll<Permission>();
        }

        public async Task<Permission> GetPermission(Guid permissionId)
        {
            return  await permissionRepository.FindById(permissionId);
        }

        public async Task<Permission> GetPermissionByRole(Guid roleId)
        {
            return (Permission)await permissionRepository.Get(
                p => p.Roles.Any(r => r.Id == roleId));
        }

        public async Task<Permission> GetPermissionByUser(Guid userId)
        {
            //// Assuming a user's permissions are derived from their assigned roles
            //var Roles = await GetRoleByUser(userId);
            //var permissions = Roles.SelectMany(r => r.Permissions).Distinct();
            //return permissions.FirstOrDefault(); 
            throw new NotImplementedException();
        }

        public async Task AddPermission(Permission permission)
        {
            await permissionRepository.Add(permission);
            await permissionRepository.SaveChangesAsync();
        }

        public async Task UpdatePermission(Permission permission)
        {
            await permissionRepository.Update(permission);
            await permissionRepository.SaveChangesAsync();
        }

        public async Task RemovePermission(Guid permissionId)
        {
            var permission = await GetPermission(permissionId);
            if (permission != null)
            {
                await permissionRepository.Delete(permission);
                await permissionRepository.SaveChangesAsync();
            }
        }
        public async Task AssignPermissionToRole(Guid permissionId, Guid roleId)
        {
            var permission = await GetPermission(permissionId);
            var role = await GetRole(roleId);
            if (permission != null && role != null)
            {
                role.Permissions.Add(permission);
                await permissionRepository.SaveChangesAsync();
            }
        }

        public async Task UnassignPermissionFromRole(Guid permissionId, Guid roleId)
        {
            var permission = await GetPermission(permissionId);
            var role = await GetRole(roleId);
            if (permission != null && role != null)
            {
                role.Permissions.Remove(permission);
                await permissionRepository.SaveChangesAsync();
            }
        }
        #endregion
    }
}
