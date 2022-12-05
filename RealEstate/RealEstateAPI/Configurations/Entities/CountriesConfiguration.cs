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
    public class CountriesConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                new Country
                {
                    Id = 1,
                    Title = "Lithuania",
                    Date = DateTime.Now
                }
            );
        }
    }
}
