using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreatePropertyTypeDTO
    {
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    public class UpdatePropertyTypeDTO : CreatePropertyTypeDTO
    {
    }
    public class PropertyTypeDTO : CreatePropertyTypeDTO
    {
        public int Id { get; set; }
        public virtual IList<PropertyDTO> Properties { get; set; }
    }
}
