using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Data;
using System;

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
                    Title = "Hotels",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 2,
                    Title = "Apartments",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 3,
                    Title = "Resorts",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 4,
                    Title = "Villas",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 5,
                    Title = "Cabins",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 6,
                    Title = "Houses",
                    Date = DateTime.Now
                },
                new PropertyType
                {
                    Id = 7,
                    Title = "Lands",
                    Date = DateTime.Now
                }
            ); ;
        }
    }
}
