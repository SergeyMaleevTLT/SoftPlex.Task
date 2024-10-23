using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Dto;

public record ProductPostDto(
    [Required][MaxLength(255)]
    string Name,
    string Description
    );
