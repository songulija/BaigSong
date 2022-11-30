using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAPI.Data
{
    /// <summary>
    /// Payment class will have fields as status,updatetime,emailadress
    /// that will come from paypal, when you make payment. ForeignKey is productId
    /// </summary>
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        [NotMapped]
        public User User { get; set; }
        [ForeignKey(nameof(Property))]
        public int? PropertyId { get; set; }
        [NotMapped]
        public Property Property { get; set; }
        public float Price { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
        public string UpdateTime { get; set; }
        public string EmailAdress { get; set; }
    }
}
