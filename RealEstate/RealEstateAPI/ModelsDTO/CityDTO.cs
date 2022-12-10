using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateCityDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int? CountryId { get; set; }
        public byte[] Photo { get; set; }
        public IFormFile File { get; set; }
    }
    public class UpdateCityDTO : CreateCityDTO
    {
    }
    public class CityDTO : CreateCityDTO
    {
        public int Id { get; set; }
        public virtual CountryDTO Country { get; set; }
        public virtual IList<PropertyDTO> Properties { get; set; }
    }
}
