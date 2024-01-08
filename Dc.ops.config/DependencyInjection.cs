using Dc.ops.Entities;
using Dc.ops.Manager.Managers;
using Dc.Ops.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            // Dc Ops DbContext
            services.AddDbContext<DcOpsDbContext>(options =>
                options.UseSqlServer("Server=(localdb)\\dcops;Database=Dc_Ops;Trusted_Connection=True;"));
            //Ops Repository:
            services.AddScoped(typeof(OpsRep<>));
            //Equipment:
            
          
            services.AddScoped<EquipmentManager>();
            //Equipment Type:
           
          
            services.AddScoped<EquipmentTypeManager>();
            //User:
            
            services.AddScoped<UserManager>();
           
            //Equipment History:

            services.AddScoped<EquipmentHistoryManager>();
            
            //Assign Location:

            services.AddScoped<AssignLocationManager>();
           
            //Assign History:

            services.AddScoped<AssignHistoryManager>();
           

            // Register other services, repositories, managers, etc.

            return services;
        }
    }
}

