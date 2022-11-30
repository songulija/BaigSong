using RealEstateAPI.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [ForeignKey(nameof(UserType))]
        public int? TypeId { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual IList<Property> Properties { get; set; }
        public virtual IList<Comment> Comments { get; set; }
        public virtual IList<FavouriteProperty> FavouriteProperties { get; set; }
        public virtual IList<Journal> Journals { get; set; }
        public virtual IList<Payment> Payments { get; set; }
    }
}
