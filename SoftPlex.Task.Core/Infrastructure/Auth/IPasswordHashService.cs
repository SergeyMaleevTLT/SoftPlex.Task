namespace SoftPlex.Task.Core.Infrastructure.Auth;

public interface IPasswordHashService
{
    /// <summary>
    /// Хеширует пароль
    /// </summary>
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    /// <summary>
    /// Проверяет введеный пользователем пароль с хешем хранящимся в базе данных
    /// </summary>
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}