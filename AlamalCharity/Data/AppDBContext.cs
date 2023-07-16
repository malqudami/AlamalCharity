using AlamalCharity.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AlamalCharity.Data
{
    public partial class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<FAMILIES> Families { get; set; }

        public DbSet<Subscriptions> Subscriptions { get; set; }

    }
}
