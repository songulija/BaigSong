using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateRentTypeDTO
    {
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    public class UpdateRentTypeDTO : CreateRentTypeDTO
    {
    }
    public class RentTypeDTO : CreateRentTypeDTO
    {
        public int Id { get; set; }
        public virtual IList<PropertyDTO> Properties { get; set; }
    }
}
