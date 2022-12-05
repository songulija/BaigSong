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
                   PropertyTypeId = 2,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Gedimino g. 71",
                   Title = "Vilnius G71",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 350,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 2,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Gedimino g. 72",
                   Title = "Vilnius G72",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 369,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 3,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Gedimino g. 73",
                   Title = "Vilnius K73",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 400,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 4,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Gedimino g. 74",
                   Title = "Vilnius K74",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 350,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 5,
                   UserId = 1,
                   PropertyTypeId = 2,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Gedimino g. 75",
                   Title = "Vilnius K75",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 500,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 6,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 10",
                   Title = "Vilnius R10",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 600,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 7,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 11",
                   Title = "Vilnius R11",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 550,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 8,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 12",
                   Title = "Vilnius R12",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 589,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 9,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 13",
                   Title = "Vilnius R13",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 600,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 10,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 14",
                   Title = "Vilnius R14",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 650,
                   Date = DateTime.Now
               },
               new Property
               {
                   Id = 11,
                   UserId = 1,
                   PropertyTypeId = 4,
                   RentTypeId = 1,
                   CityId = 1,
                   Address = "Rygos g. 15",
                   Title = "Vilnius R15",
                   Description = "Certainty listening no no behaviour existence assurance situation is. Because add why not esteems amiable him. Interested the unaffected mrs law friendship add principles. Indeed on people do merits to. Court heard which up above hoped grave do. Answer living law things either sir bed length. Looked before we an on merely. These no death he at share alone. Yet outward the him compass hearted are tedious.",
                   RoomNumber = 2,
                   Price = 550,
                   Date = DateTime.Now
               }
            );
        }
    }
}
