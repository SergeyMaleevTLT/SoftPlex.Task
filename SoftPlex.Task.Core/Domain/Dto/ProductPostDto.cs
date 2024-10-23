using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Domain.Dto;

public record ProductPostDto(
    [Required][MaxLength(255, ErrorMessage = "")]
    string Name,
    string Description
    );