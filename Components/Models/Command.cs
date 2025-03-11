using System.ComponentModel.DataAnnotations;

namespace VSHCTwebApp.Components.Models
{
    public class Command
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название команды")]
        [StringLength(50, ErrorMessage = "Название команды не должно превышать 50 символов")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите описание команды")]
        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите описание команды")]
        public string Members { get; set; } = string.Empty;
    }
} 