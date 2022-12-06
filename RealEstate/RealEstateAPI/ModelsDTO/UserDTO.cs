using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class UpdateUserDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int TypeId { get; set; }
    }
    /// <summary>
    /// UserDTO has to have same fields as User. It also inherits from LoginDTO
    /// Also user can have Roles(user or admin). 
    /// </summary>
    public class UserDTO : LoginUserDTO
    {
        [Required]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
        public virtual IList<PropertyDTO> Properties { get; set; }
        public virtual IList<CommentDTO> Comments { get; set; }
        public virtual IList<FavouritePropertyDTO> FavouriteProperties { get; set; }
        public virtual IList<JournalDTO> Journals { get; set; }
        public virtual IList<PaymentDTO> Payments { get; set; }
    }

    public class DisplayUserDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public int TypeId { get; set; }
        public UserTypeDTO UserType { get; set; }
    }
}
