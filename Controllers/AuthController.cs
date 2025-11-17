using Exaln.Interfaces;
using Exaln.Models;
using Exaln.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AuthController(UserManager<IdentityUser> userManager, IConfiguration configuration, ITokenService tokenService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status409Conflict, "User is already exists.");
        }

        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email
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
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var token = _tokenService.CreateToken(user);
            return Ok(new
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            });
        }
        return Unauthorized("Email or password is incorrect.");
    }


}