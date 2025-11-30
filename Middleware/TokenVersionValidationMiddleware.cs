using Microsoft.AspNetCore.Http;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;



namespace Exaln.Middleware
{
    public class TokenVersionValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConnectionMultiplexer _redis;

        public TokenVersionValidationMiddleware(RequestDelegate next, IConnectionMultiplexer redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task Invoke(HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

            if (authHeader == null || !authHeader.StartsWith("Bearer "))
            {
                await _next(context);
                return;
            }

            var token = authHeader.Substring("Bearer".Length);

            var handler = new JwtSecurityTokenHandler();

            if (!handler.CanReadToken(token))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var jwt = handler.ReadJwtToken(token);

            var jwtVersion = jwt.Claims.FirstOrDefault(c => c.Type == "token_version")?.Value;

            var userId = jwt.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;

            if (jwtVersion == null || userId == null)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            var redisDb = _redis.GetDatabase();
            var redisKey = $"user:{userId}:token_version";

            var redisVersion = await redisDb.StringGetAsync(redisKey);
            if (!redisVersion.HasValue)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            if (redisVersion.ToString() != jwtVersion)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }

            await _next(context);

        }

    }
}
