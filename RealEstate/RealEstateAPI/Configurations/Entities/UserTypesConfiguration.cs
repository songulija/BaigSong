using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Data;

namespace RealEstateAPI.Configurations.Entities
{
    public class UserTypesConfiguration : IEntityTypeConfiguration<UserType>
    {
        public void Configure(EntityTypeBuilder<UserType> builder)
        {
            builder.HasData(
               new UserType
               {
                   Id = 1,
                   Title = "ADMINISTRATOR"
               },
               new UserType
               {
                   Id = 2,
                   Title = "USER"
               }
            );
        }
    }
}
