using Exaln.DTO.AccountDTO;
using Exaln.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace Exaln.Interfaces
{
    public interface ITokenService
    {
        public Task<AuthResponseDTO> CreateTokenAsync(ApplicationUser user, HttpResponse response);
        public string GenerateRefreshToken();
    }
}
