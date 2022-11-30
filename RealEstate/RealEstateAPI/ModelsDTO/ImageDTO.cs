using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateImageDTO
    {
        public int? PropertyId { get; set; }
        [Required]
        public byte[] Photo { get; set; }
    }
    public class UpdateImageDTO : CreateImageDTO
    {
    }
    public class ImageDTO : CreateImageDTO
    {
        public int Id { get; set; }
        public PropertyDTO Property { get; set; }
    }
}
