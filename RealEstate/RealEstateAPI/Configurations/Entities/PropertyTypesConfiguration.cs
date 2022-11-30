using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Data;

namespace RealEstateAPI.Configurations.Entities
{ /// <summary>
  /// Letting BrandConfiguration class know that it'll be inhereted from
  /// IEntityTypeConfiguration of type Brand
  /// here we will define our initial Brands for database
  /// </summary>
    public class PropertyTypesConfiguration : IEntityTypeConfiguration<PropertyType>
    {
        public void Configure(EntityTypeBuilder<PropertyType> builder)
        {
            builder.HasData(
                new PropertyType
                {
                    Id = 1,
                    Title = "Flat"
                },
                new PropertyType
                {
                    Id = 2,
                    Title = "House"
                },
                new PropertyType
                {
                    Id = 3,
                    Title = "Land"
                },
                new PropertyType
                {
                    Id = 4,
                    Title = "Car"
                }
            );
        }
    }
}
