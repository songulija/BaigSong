using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        /*public string Photo { get; set; }*/
        public virtual IList<City> Cities { get; set; }
    }
}
