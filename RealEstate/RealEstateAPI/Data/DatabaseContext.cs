using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Configurations.Entities;

namespace RealEstateAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavouriteProperty> FavouriteProperties { get; set; }
        public DbSet<Journal> Journals { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<RentType> RentTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //applying configurations that i created. Seed data for Categories, brands
            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new UserTypesConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new PropertyTypesConfiguration());
            builder.ApplyConfiguration(new RentTypesConfiguration());
            builder.ApplyConfiguration(new CountriesConfiguration());
            builder.ApplyConfiguration(new CitiesConfiguration());
            builder.ApplyConfiguration(new PropertiesConfiguration());
            builder.ApplyConfiguration(new FavouritePropertiesConfiguration());
        }
    }
}
