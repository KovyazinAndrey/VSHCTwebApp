using System.ComponentModel.DataAnnotations;

namespace VSHCTwebApp.Components.Models
{
    public class Note
    {
        [Required(ErrorMessage = "Название обязательно")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Описание обязательно")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Имя создателя обязательно")]
        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
