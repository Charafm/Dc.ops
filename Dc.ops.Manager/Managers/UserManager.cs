using Dc.ops.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
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
        public UserManager(OpsRep<User> userRepository, OpsRep<UserRole> roleRepository, OpsRep<Permission> permissionRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
            this.permissionRepository = permissionRepository;
        }
        #region User
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return  userRepository.GetAll<User>();
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return  userRepository.FindById(id);
        }


        public async Task<User> CreateUserAsync(User user)
        {
            //No hashing till everything is Done!
            userRepository.Add(user);
            await userRepository.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            userRepository.Update(user);
            await userRepository.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            userRepository.Delete(id);
            await userRepository.SaveChangesAsync();
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
            return new User { Username = "TemporaryUser" }; // hard coded solution for early testing 
        }
        #endregion

        #region Role
        public async Task<IEnumerable<UserRole>> GetAllRoles()
        {
            return  roleRepository.GetAll<UserRole>();
        }

        public async Task<UserRole> GetRole(Guid roleId)
        {
            return  roleRepository.FindById(roleId);
        }

        public async Task<UserRole> GetRoleByPermission(Guid permissionId)
        {
            return (UserRole)roleRepository.Get(
                r => r.Permissions.Any(p => p.Id == permissionId));
        }

        public async Task<UserRole> GetRoleByUser(Guid userId)
        {
            return (UserRole)roleRepository.Get(
                r => r.Users.Any(u => u.Id == userId));
        }

        public async Task AddRole(UserRole role)
        {
             roleRepository.Add(role);
            await roleRepository.SaveChangesAsync();
        }

        public async Task UpdateRole(UserRole role)
        {
            roleRepository.Update(role);
            await roleRepository.SaveChangesAsync();
        }

        public async Task RemoveRole(Guid roleId)
        {
            var role = await GetRole(roleId);
            if (role != null)
            {
                roleRepository.Delete(role);
                await roleRepository.SaveChangesAsync();
            }
        }

        public async Task AssignRoleToUser(Guid roleId, Guid userId)
        {
            var user =  userRepository.FindById(userId);
            
            var role = await GetRole(roleId);
            if (user != null && role != null)
            {
                user.Role = role;
                userRepository.Update(user);
                await roleRepository.SaveChangesAsync();
            }
        }

        public async Task UnassignRoleFromUser(Guid roleId, Guid userId)
        {
            var user =  userRepository.FindById(userId);
            var role = await GetRole(roleId);
            if (user != null && role != null)
            {
                if(user.Role != null && user.Role == role)
                {
                    user.Role = null;
                    userRepository.Update(user);
                    await roleRepository.SaveChangesAsync();
                }
                
            }
        }

        #endregion

        #region Permission
        public async Task<IEnumerable<Permission>> GetAllPermissions()
        {
            return  permissionRepository.GetAll<Permission>();
        }

        public async Task<Permission> GetPermission(Guid permissionId)
        {
            return  permissionRepository.FindById(permissionId);
        }

        public async Task<Permission> GetPermissionByRole(Guid roleId)
        {
            return (Permission)permissionRepository.Get(
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
            permissionRepository.Add(permission);
            await permissionRepository.SaveChangesAsync();
        }

        public async Task UpdatePermission(Permission permission)
        {
            permissionRepository.Update(permission);
            await permissionRepository.SaveChangesAsync();
        }

        public async Task RemovePermission(Guid permissionId)
        {
            var permission = await GetPermission(permissionId);
            if (permission != null)
            {
                permissionRepository.Delete(permission);
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
