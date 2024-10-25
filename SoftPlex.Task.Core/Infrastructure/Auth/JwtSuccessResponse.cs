namespace SoftPlex.Task.Core.Infrastructure.Auth;

public class JwtSuccessResponse
{
    public int Lifetime { get; set; }

    public string Token { get; set; }
}