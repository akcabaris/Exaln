using Exaln.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Controllers
{
    [Route("ielts/")]
    [ApiController]
    public class IELTSReadingController : Controller
    {
        private readonly IIELTSReadingRepository _ieltsReadingRepository;
        private readonly IRedisService _redis;

        public IELTSReadingController(IIELTSReadingRepository ieltsReadingRepository, IRedisService redis)
        {
            _ieltsReadingRepository = ieltsReadingRepository;
            _redis = redis;
        }

        [HttpGet("reading-practices")]
        //[Authorize]
        public async Task<IActionResult> GetReadingPractices()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrWhiteSpace(refreshToken))
                return Unauthorized("Refresh token missing");

            var refreshKey = $"refresh:{refreshToken}";
            var userId = await _redis.GetStringAsync(refreshKey);

            if (userId == null)
            {
                return Unauthorized("Unauthorized.");
            }


            var result = await _ieltsReadingRepository.GetReadingPracticeListAsync(userId, ExamType.GeneralTrainingPractice);

            return Ok(result);
        }

    }
}
