using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.Data
{
    public class UserType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        public virtual IList<User> Users { get; set; }
    }
}
