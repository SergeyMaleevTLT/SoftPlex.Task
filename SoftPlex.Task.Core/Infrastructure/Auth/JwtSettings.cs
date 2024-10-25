namespace SoftPlex.Task.Core.Infrastructure.Auth;

public class JwtSettings
{
    /// <summary>
    /// Секретный ключ для верификации
    /// </summary>
    public string Secret { get; set; }

    /// <summary>
    /// Время жизни токена (сек.)
    /// </summary>
    public int LifeTimeToken { get; set; }
}