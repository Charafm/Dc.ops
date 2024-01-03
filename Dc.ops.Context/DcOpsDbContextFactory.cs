
using Dc.Ops.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Dc.ops.Context
{

    public class DcOpsDbContextFactory : IDesignTimeDbContextFactory<DcOpsDbContext>
    {
        public DcOpsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DcOpsDbContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\dcops;Database=Dc_Ops;Trusted_Connection=True;");

            return new DcOpsDbContext(optionsBuilder.Options);
        }
    }
}