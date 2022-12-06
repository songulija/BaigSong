using AutoMapper;
using RealEstateAPI.Data;
using RealEstateAPI.ModelsDTO;

namespace RealEstateAPI.Configurations
{
    /// <summary>
    /// Inherits from Profile(automapper). we have constructor. In constructor
    /// we have to define all the Mappings. Domain class Brand is going to map 
    /// to BrandDTO, CreateBrandDTO, UpdateBrandDTO and from them back to Brand domain object
    /// </summary>
    public class MapperInitilizer : Profile
    {
        public MapperInitilizer()
        {
            CreateMap<UserType, UserTypeDTO>();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();
            CreateMap<User, DisplayUserDTO>().ReverseMap();


            CreateMap<PropertyType, PropertyTypeDTO>().ReverseMap();
            CreateMap<PropertyType, CreatePropertyTypeDTO>().ReverseMap();
            CreateMap<PropertyType, UpdatePropertyTypeDTO>().ReverseMap();

            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            CreateMap<Country, UpdateCountryDTO>().ReverseMap();

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<City, CreateCityDTO>().ReverseMap();
            CreateMap<City, UpdateCityDTO>().ReverseMap();

            CreateMap<RentType, RentTypeDTO>().ReverseMap();
            CreateMap<RentType, CreateRentTypeDTO>().ReverseMap();
            CreateMap<RentType, UpdateRentTypeDTO>().ReverseMap();

            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Property, CreatePropertyDTO>().ReverseMap();
            CreateMap<Property, UpdatePropertyDTO>().ReverseMap();

            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CreateCommentDTO>().ReverseMap();
            CreateMap<Comment, UpdateCommentDTO>().ReverseMap();

            CreateMap<FavouriteProperty, FavouritePropertyDTO>().ReverseMap();
            CreateMap<FavouriteProperty, CreateFavouritePropertyDTO>().ReverseMap();
            CreateMap<FavouriteProperty, UpdateFavouritePropertyDTO>().ReverseMap();

            CreateMap<Journal, JournalDTO>().ReverseMap();
            CreateMap<Journal, CreateJournalDTO>().ReverseMap();
            CreateMap<Journal, UpdateJournalDTO>().ReverseMap();

            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Payment, CreatePaymentDTO>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDTO>().ReverseMap();
        }

    }
}
