using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Infrastructure.Data.Context;

namespace SoftPlex.Task.Core.Infrastructure.Auth.Impl;

internal class AuthJwtService : IAuthService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;
    private readonly IPasswordHashService _passwordHashService;
    private readonly JwtSettings _settings;
    
    
    public AuthJwtService(IMapper mapper, DataContext dataContext, IPasswordHashService passwordHashService, JwtSettings settings)
    {
        _mapper = mapper;
        _dataContext = dataContext;
        _passwordHashService = passwordHashService;
        _settings = settings;
    }

    public async Task<UserDto?> AddUserAuthServiceAsync(UserLoginDto user, CancellationToken cancel = default)
    {
        _passwordHashService.CreatePasswordHash(user.Password, out var passwordHash, out var passwordSalt);

        var newUser = new User { Id = Guid.NewGuid(), Login = user.Login, PasswordHash = passwordHash, PasswordSalt = passwordSalt };
        await _dataContext.User.AddAsync(newUser, cancel);
        await _dataContext.SaveChangesAsync(cancel);
        return await GetUserAuthServiceAsync(user, cancel);
    }

    public Task<bool> AnyUserByLoginAuthServiceAsync(string login, CancellationToken cancel = default)
    {
        return _dataContext.User.AnyAsync(x => x.Login == login, cancel);
    }

    public async Task<UserDto?> GetUserAuthServiceAsync(UserLoginDto user, CancellationToken cancel = default)
    {
        var userDb = await _dataContext.User.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == user.Login, cancel);

        return userDb is not null
            ? _mapper.Map<UserDto>(userDb)
            : null;
    }

    public async Task<bool> SecurityPasswordCheck(UserDto user, string password, CancellationToken cancel = default)
    {
        var userDb = await _dataContext.User.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Login == user.Login, cancel);

        return userDb is not null && _passwordHashService.VerifyPasswordHash(password, userDb.PasswordHash, userDb.PasswordSalt);
    }

    public Task<JwtSuccessResponse> AuthenticateAsync(UserDto user, CancellationToken cancel = default)
    {
        var token = GenerateJwtAsync(user, _settings.LifeTimeToken);
        return System.Threading.Tasks.Task.FromResult(new JwtSuccessResponse
        {
            Lifetime = _settings.LifeTimeToken,
            Token = token
        });
    }
    
    /// <summary>
    /// Генерирует токен для найденого пользователя
    /// </summary>
    /// <param name="user"></param>
    /// <param name="lifetime"></param>
    /// <param name="rightsInContragents"></param>
    /// <returns></returns>
    private string GenerateJwtAsync(UserDto user, int lifetime)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_settings.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            }),

            Expires = DateTime.UtcNow.AddSeconds(lifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}