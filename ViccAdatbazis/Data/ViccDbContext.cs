using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Data
{
    public class ViccDbContext : DbContext
    {
        public ViccDbContext(DbContextOptions<ViccDbContext> options) : base(options)
        {
            
        }
        public DbSet<Vicc> Viccek {  get; set; }
    }
}
