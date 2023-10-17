using Microsoft.EntityFrameworkCore;
using WebApplication5.Domain.Entity;

namespace WebApplication5.Domain
{
    public class DataContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataContext()
        {
                
        }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
