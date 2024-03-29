using Microsoft.EntityFrameworkCore;
using PregaDays.Models.Domain;

namespace PregaDays.Data
{
    public class PregaDaysDbContext : DbContext
    {
        public PregaDaysDbContext(DbContextOptions<PregaDaysDbContext> dbContextOptions) : base(dbContextOptions)
        {
                
        }
        public DbSet<Day> Days { get; set; }

    }
}
