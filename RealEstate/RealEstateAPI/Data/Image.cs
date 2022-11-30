using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAPI.Data
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(Property))]
        public int? PropertyId { get; set; }
        [NotMapped]
        public Property Property { get; set; }
        public byte[] Photo { get; set; }
    }
}
