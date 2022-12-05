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
    public class CitiesConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(
                new City
                {
                    Id = 1,
                    Title = "Vilnius",
                    Date = DateTime.Now,
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Title = "Kaunas",
                    Date = DateTime.Now,
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Title = "Klaipėda",
                    Date = DateTime.Now,
                    CountryId = 1
                },
                new City
                {
                    Id = 4,
                    Title = "Palanga",
                    Date = DateTime.Now,
                    CountryId = 1
                },
                new City
                {
                    Id = 5,
                    Title = "Šiauliai",
                    Date = DateTime.Now,
                    CountryId = 1
                },
                new City
                {
                    Id = 6,
                    Title = "Druskininkai",
                    Date = DateTime.Now,
                    CountryId = 1
                }
            );
        }
    }
}
