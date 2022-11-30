using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    public class Property
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(PropertyType))]
        public int? PropertyTypeId { get; set; }
        public virtual PropertyType PropertyType { get; set; }

        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? RoomNumber { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<Payment> Payments { get; set; }
        public virtual IList<Journal> Journals { get; set; }
        public virtual IList<FavouriteProperty> FavouriteObjects { get; set; }
        public virtual IList<Image> Images { get; set; }
    }
}
