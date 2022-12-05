using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateCountryDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
    public class UpdateCountryDTO : CreateCountryDTO
    {
    }
    public class CountryDTO : CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<CityDTO> Cities { get; set; }
    }
}
