using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Data;
using System;

namespace RealEstateAPI.Configurations.Entities
{
    /// <summary>
    /// Letting CategoriesConfiguration class know that it'll be inhereted from
    /// IEntityTypeConfiguration of type Category
    /// here we will define our initial Categories for database
    /// </summary>
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Lukas",
                    LastName = "Songulija",
                    Email = "lsongulija@gmail.com",
                    PhoneNumber = "+37061115217",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    TypeId = 1
                },
                new User
                {
                    Id = 2,
                    FirstName = "Karolis",
                    LastName = "Pigaga",
                    Email = "kpigaga@gmail.com",
                    PhoneNumber = "+37061115982",
                    Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                    TypeId = 1
                }
            );
        }
    }
}
