using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Configurations.Entities;

namespace RealEstateAPI.Data
{
    /// <summary>
    /// DatabaseContext inherits from IdentityDbContext and i'm adding
    /// ApiUser as its context. basically DbContext relative to ApiUser becouse
    /// by default it gonna use identity user
    /// </summary>
    public class DatabaseContext : DbContext
    {
        /**
        * Create a constructor. With DbContextOptions 
        * these options can be used to configure DataContext
        * And be use base class constructor. When using ApplicationDbContext class
        * we will have to provide those options to this constructor 
        * then it'll go to constructor of ApplicationDbContext class
        */
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
        {
        }

        //With DbSets we define tables in our database. we can get and set smth to those tables
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //applying configurations that i created. Seed data for Categories, brands
            builder.ApplyConfiguration(new RolesConfiguration());
            builder.ApplyConfiguration(new UserTypesConfiguration());
            builder.ApplyConfiguration(new UsersConfiguration());
            builder.ApplyConfiguration(new PropertyTypesConfiguration());
            builder.ApplyConfiguration(new RentTypesConfiguration());
            //applying RoleConfiguration. To add two user roles
            builder.ApplyConfiguration(new PropertiesConfiguration());
        }
    }
}
