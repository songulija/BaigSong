using System;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateFavouritePropertyDTO
    {
        public int UserId { get; set; }
        public int PropertyId { get; set; }
        public string Date { get; set; }
    }

    public class UpdateFavouritePropertyDTO : CreateFavouritePropertyDTO
    {
    }

    public class FavouritePropertyDTO : CreateFavouritePropertyDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PropertyDTO Property { get; set; }
    }
}
