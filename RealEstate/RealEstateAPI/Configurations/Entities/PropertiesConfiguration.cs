using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAPI.Data;
using System;

namespace RealEstateAPI.Configurations.Entities
{
    public class PropertiesConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasData(
               new Property
               {
                   Id = 1,
                   UserId = 1,
                   PropertyTypeId = 1,
                   RentTypeId = 2,
                   Country = "Lithuania",
                   City = "Vilnius",
                   Address = "Gedimino g. 78",
                   Title = "Vilnius Apartments & Suites - Town Hall",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 50,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 2,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 2,
                   Country = "Lithuania",
                   City = "Vilnius",
                   Address = "Gedimino g. 78",
                   Title = "Vilnius Apartments & Suites - Village Hall",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 50,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 3,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 2,
                   Country = "Lithuania",
                   City = "Vilnius",
                   Address = "Gedimino g. 78",
                   Title = "Vilnius Apartments & Suites - Karolis Hall",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 50,
                   Date = DateTime.Now
               }
            );
        }
    }
}
