using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAPI.Data
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(User))]
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey(nameof(Property))]
        public int? PropertyId { get; set; }
        public virtual Property Property { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
