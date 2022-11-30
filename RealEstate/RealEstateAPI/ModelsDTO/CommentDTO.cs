using System;
using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.ModelsDTO
{
    public class CreateCommentDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int PropertyId { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }

    public class UpdateCommentDTO : CreateCommentDTO
    {

    }

    public class CommentDTO : CreateCommentDTO
    {
        public int Id { get; set; }
        public UserDTO User { get; set; }
        public PropertyDTO Property { get; set; }

    }
}
