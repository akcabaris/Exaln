using Exaln.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("readingSections/{examID}")]
        //[Authorize]
        public async Task<IActionResult> GetReadingSections(int examID)
        {
            var result = await _ieltsReadingRepository.GetReadingSectionListAsync(examID);

            return Ok(result);
        }

        [HttpGet("readingSectionParts/{sectionID}")]
        //[Authorize]
        public async Task<IActionResult> GetReadindSectionParts(int sectionID)
        {
            if (!ModelState.IsValid || sectionID < 1)
            {
                return BadRequest();
            }

            var result = await _ieltsReadingRepository.GetReadingSectionPartListAsync(sectionID);

            return Ok(result);
        }

        [HttpGet("readingPractices")]
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


            var result = await _ieltsReadingRepository.GetReadingPracticeListAsync(userId, ExamType.General_Training_Practice);

            return Ok(result);
        }

    }
}
