using Assets.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Assets.Persistence.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<AssetEntity> Assets { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}