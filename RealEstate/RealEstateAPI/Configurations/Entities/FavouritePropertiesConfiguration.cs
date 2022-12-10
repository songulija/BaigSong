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
    public class FavouritePropertiesConfiguration : IEntityTypeConfiguration<FavouriteProperty>
    {
        public void Configure(EntityTypeBuilder<FavouriteProperty> builder)
        {
            builder.HasData(
                new FavouriteProperty
                {
                    Id = 1,
                    PropertyId = 1,
                    Date = DateTime.Now,
                    UserId = 1
                },
                new FavouriteProperty
                {
                    Id = 2,
                    PropertyId = 2,
                    Date = DateTime.Now,
                    UserId = 1
                },
                new FavouriteProperty
                {
                    Id = 3,
                    PropertyId = 3,
                    Date = DateTime.Now,
                    UserId = 1
                },
                new FavouriteProperty
                {
                    Id = 4,
                    PropertyId = 1,
                    Date = DateTime.Now,
                    UserId = 2
                },
                //1,2,3,4,5,12
                new FavouriteProperty
                {
                    Id = 5,
                    PropertyId = 2,
                    Date = DateTime.Now,
                    UserId = 2
                },
                new FavouriteProperty
                {
                    Id = 6,
                    PropertyId = 4,
                    Date = DateTime.Now,
                    UserId = 2
                },
                new FavouriteProperty
                {
                    Id = 7,
                    PropertyId = 5,
                    Date = DateTime.Now,
                    UserId = 2
                },
                new FavouriteProperty
                {
                    Id = 8,
                    PropertyId = 4,
                    Date = DateTime.Now,
                    UserId = 3
                },
                new FavouriteProperty
                {
                    Id = 9,
                    PropertyId = 4,
                    Date = DateTime.Now,
                    UserId = 4
                },
                new FavouriteProperty
                {
                    Id = 10,
                    PropertyId = 2,
                    Date = DateTime.Now,
                    UserId = 4
                }
            );
        }
    }
}
