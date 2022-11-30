using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RealEstateAPI.Data
{
    /// <summary>
    /// Journal has userId and productId foreign keys
    /// journals table saves if item was SEEN, ADDED TO WHISHLIST
    /// Thats a Type
    /// </summary>
    public class Journal
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
        public DateTime Date { get; set; }
        public string Type { get; set; }

    }
}
