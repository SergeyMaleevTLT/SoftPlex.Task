using System.ComponentModel.DataAnnotations;

namespace SoftPlex.Task.Core.Domain.Dto;

public record UserLoginDto(
    [Required, StringLength(255, MinimumLength = 3)]
    string Login,
    [Required]
    string Password
);   