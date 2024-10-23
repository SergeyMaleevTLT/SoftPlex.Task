using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Dto;

public record ProductDto(
    Guid Id,
    [Required] [MaxLength(255)]
    string Name,
    string Description
    );