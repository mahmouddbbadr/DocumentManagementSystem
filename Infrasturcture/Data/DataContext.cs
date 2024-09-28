using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Emit;

namespace Infrasturcture.Data
{
    public class DataContext : IdentityDbContext 
    {
        public DataContext()
        {
            
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<WorkSpace> WorkSpaces { get; set; }
        public DbSet<Domain.Models.Directory> Directories { get; set; }
        public DbSet<Domain.Models.Document> Documents { get; set; }
        public DbSet<Tag> Tags { get; set; }
        

    }
}
