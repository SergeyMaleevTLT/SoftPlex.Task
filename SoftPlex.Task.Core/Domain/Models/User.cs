using SoftPlex.Task.Core.Domain.Models.Common;

namespace SoftPlex.Task.Core.Domain.Models;

/// <summary>
/// root: root_pwd_18
/// </summary>
public class User : BaseEntity
{
    public string Login { get; set; }

    public byte[] PasswordSalt { get; set; }

    public byte[] PasswordHash { get; set; }
}