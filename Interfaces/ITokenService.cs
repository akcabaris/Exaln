using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Exaln.Interfaces
{
    public interface ITokenService
    {
        public JwtSecurityToken CreateToken(IdentityUser user);
    }
}
