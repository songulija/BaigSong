using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreatePropertyDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PropertyTypeId { get; set; }
        [Required]
        public int RentTypeId { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int RoomNumber { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public byte[] Photo { get; set; }
        public IFormFile File { get; set; }
    }

    public class UpdatePropertyDTO : CreatePropertyDTO
    {
    }

    public class PropertyDTO : CreatePropertyDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PropertyTypeDTO PropertyType { get; set; }
        public RentTypeDTO RentType { get; set; }
        public CityDTO City { get; set; }
        public int NumberOfLikes { get; set; }
        public int SeenNumber { get; set; }
        public bool Liked { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<PaymentDTO> Payments { get; set; }
        public virtual IList<JournalDTO> Journals { get; set; }
        public virtual IList<FavouritePropertyDTO> FavouriteObjects { get; set; }
        public virtual IList<ImageDTO> Images { get; set; }
    }
}
