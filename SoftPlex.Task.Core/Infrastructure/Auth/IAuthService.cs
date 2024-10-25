using SoftPlex.Task.Core.Domain.Dto;

namespace SoftPlex.Task.Core.Infrastructure.Auth;

public interface IAuthService
{
    /// <summary>
    /// Добавить нового пользователя.
    /// </summary>
    Task<UserDto?> AddUserAuthServiceAsync(UserLoginDto user, CancellationToken cancel = default);
    
    /// <summary>
    /// Проверка пользователя в системе.
    /// </summary>
    Task<bool> AnyUserByLoginAuthServiceAsync(string login, CancellationToken cancel = default);
    
    /// <summary>
    /// Возвращает пользователя из базы данных сервиса.
    /// </summary>
    Task<UserDto?> GetUserAuthServiceAsync(UserLoginDto user, CancellationToken cancel = default);

    /// <summary>
    /// Проверяет пароль.
    /// </summary>
    Task<bool> SecurityPasswordCheck(UserDto user, string password, CancellationToken cancel = default);

    /// <summary>
    /// Ответ аутентификации.
    /// </summary>
    Task<JwtSuccessResponse> AuthenticateAsync(UserDto user, CancellationToken cancel = default);
}