using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Web.Models;

public class ProductPostViewModel
{
    [Required(ErrorMessage = "Поле является обязательным")]
    [MaxLength(255, ErrorMessage = "Не более 255 символов")]
    public string Name { get; set; }
    
    public string? Description { get; set; }
}