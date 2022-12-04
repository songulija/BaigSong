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
    public class RentTypesConfiguration : IEntityTypeConfiguration<RentType>
    {
        public void Configure(EntityTypeBuilder<RentType> builder)
        {
            builder.HasData(
                new RentType
                {
                    Id = 1,
                    Title = "Long Term",
                    Date = DateTime.Now
                },
                new RentType
                {
                    Id = 2,
                    Title = "Short Term",
                    Date = DateTime.Now
                }
            );
        }
    }
}
