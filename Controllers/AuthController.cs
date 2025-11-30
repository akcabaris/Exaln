using Exaln.DTO.AccountDTO;
using Exaln.Entities;
using Exaln.Interfaces;
using Exaln.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly IRedisService _redis;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITokenService tokenService,
        IRedisService redis)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _tokenService = tokenService;
        _redis = redis;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);

        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "User is already exists.");
        }

        ApplicationUser user = new()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            TokenVersion = 1
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "User creation failed.\"", Errors = errors });
        }

        return Ok("User created successfully!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
            return Unauthorized("Email or Password is wrong.");

        var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
        if (!result.Succeeded)
            return Unauthorized("Email or Password is wrong.");

        var tokens = await _tokenService.CreateTokenAsync(user, Response);

        return Ok(tokens);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized("Refresh token missing");

        var userIdFromRedis = await _redis.GetStringAsync($"refresh:{refreshToken}");
        if (string.IsNullOrWhiteSpace(userIdFromRedis))
            return Unauthorized("Invalid refresh token.");

        var user = await _userManager.FindByIdAsync(userIdFromRedis);
        if (user == null)
            return Unauthorized("User not found.");

        var versionKey = $"user:{userIdFromRedis}:token_version";
        var redisVersion = await _redis.GetStringAsync(versionKey);

        if (redisVersion != user.TokenVersion.ToString())
            return Unauthorized("Version mismatch.");

        await _redis.DeleteKeyAsync($"refresh:{refreshToken}");

        var newTokens = await _tokenService.CreateTokenAsync(user, Response);

        return Ok(newTokens);
    }



    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized("Refresh token missing");

        var refreshKey = $"refresh:{refreshToken}";
        var userId = await _redis.GetStringAsync(refreshKey);

        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized("Invalid refresh token");

        await _redis.DeleteKeyAsync(refreshKey);

        Response.Cookies.Append("refresh_token", "",
            new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            });

        return Ok(new { message = "Logged out" });
    }

}
