using Microsoft.EntityFrameworkCore;
using Prevent.Web.Data.Entities;

namespace Prevent.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<PreventEntity> Prevents { get; set; }
    }
}
