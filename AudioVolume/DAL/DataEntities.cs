using AudioVolume.Models;
using System.Data.Entity;

namespace AudioVolume.DAL
{
    public class DataEntities : DbContext
    {
        public DataEntities() : base("name=DataEntities")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<ConfigSite> ConfigSites { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Introduce> Introduces { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductInfo>  ProductInfos { get; set; }
        public DbSet<ProductRating>  ProductRatings { get; set; }
        public DbSet<ShowRoom> ShowRooms { get; set; }
    }
}
