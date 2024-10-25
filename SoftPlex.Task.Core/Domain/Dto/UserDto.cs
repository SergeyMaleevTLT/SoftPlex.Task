using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Domain.Dto;

public record UserDto(
    Guid Id,
    [Required, StringLength(255, MinimumLength = 3)]
    string Login
    );