using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class UserTypeDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public virtual IList<UserDTO> Users { get; set; }
    }
}
