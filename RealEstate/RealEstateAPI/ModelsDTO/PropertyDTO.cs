﻿using System;
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
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
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
    }

    public class UpdatePropertyDTO : CreatePropertyDTO
    {
    }

    public class PropertyDTO : CreatePropertyDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PropertyTypeDTO PropertyType { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<PaymentDTO> Payments { get; set; }
        public virtual IList<JournalDTO> Journals { get; set; }
        public virtual IList<FavouritePropertyDTO> FavouriteObjects { get; set; }
        public virtual IList<ImageDTO> Images { get; set; }
    }
}