using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreatePaymentDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public float Price { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string UpdateTime { get; set; }
        [Required]
        public string EmailAdress { get; set; }
    }

    public class UpdatePaymentDTO : CreatePaymentDTO
    {

    }

    /// <summary>
    /// inherits all fields from CreatePaymentDTO. When getting payment if included
    /// get Product object too
    /// </summary>
    public class PaymentDTO : CreatePaymentDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PropertyDTO Property { get; set; }
    }
}
