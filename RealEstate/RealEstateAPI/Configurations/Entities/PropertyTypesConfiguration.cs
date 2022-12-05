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
                    Date = DateTime.Now,
                    Photo = "https://cf.bstatic.com/xdata/images/xphoto/square300/57584488.webp?k=bf724e4e9b9b75480bbe7fc675460a089ba6414fe4693b83ea3fdd8e938832a6&o="
                },
                new PropertyType
                {
                    Id = 2,
                    Title = "Apartments",
                    Date = DateTime.Now,
                    Photo = "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/card-image-apartments_300/9f60235dc09a3ac3f0a93adbc901c61ecd1ce72e.jpg"
                },
                new PropertyType
                {
                    Id = 3,
                    Title = "Resorts",
                    Date = DateTime.Now,
                    Photo = "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/bg_resorts/6f87c6143fbd51a0bb5d15ca3b9cf84211ab0884.jpg"
                },
                new PropertyType
                {
                    Id = 4,
                    Title = "Houses",
                    Date = DateTime.Now,
                    Photo = "https://cf.bstatic.com/static/img/theme-index/carousel_320x240/card-image-villas_300/dd0d7f8202676306a661aa4f0cf1ffab31286211.jpg"
                },
                new PropertyType
                {
                    Id = 5,
                    Title = "Lands",
                    Date = DateTime.Now,
                    Photo = "https://cf.bstatic.com/xdata/images/city/square250/777085.webp?k=b95bc65ec83682e7aafc89112ff398b1081be9696ef92556ffd4fb9648a6b807&o="
                }
            );
        }
    }
}
