using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    public class FavouriteProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(Property))]
        public int? PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public DateTime Date { get; set; }
    }
}
