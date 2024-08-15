using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DataAccess.Model
{
    public class DataAccessContext : DbContext
    {

        public const string ConnectStrring = @"Server=DESKTOP-6HN0NTV\SQLEXPRESS;Database=ShopManageOnline;User Id=sa;Password=123;TrustServerCertificate=True;";


        public DataAccessContext()
        {
        }

        public DataAccessContext(DbContextOptions<DataAccessContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectStrring);
        }

        

    }
}
