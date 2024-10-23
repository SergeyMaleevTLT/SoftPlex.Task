using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Domain.Dto;

public record ProductDto(
    Guid Id,
    [Required] [MaxLength(255, ErrorMessage = "")]
    string Name,
    string Description
    );