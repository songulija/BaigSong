using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        /*public string Photo { get; set; }*/
        [ForeignKey(nameof(Country))]
        public int? CountryId { get; set; }
        public virtual Country Country { get; set; }
        public byte[] Photo { get; set; }
        public virtual IList<Property> Properties { get; set; }
    }
}
