namespace SoftPlex.Task.Core.Data.Context;

[Serializable]
public class ConnectionStringsSection
{
    /// <summary>
    /// Строка подключения
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// Полный путь с namespace к провайдеру базы данных
    /// </summary>
    public string Provider { get; set; }
}