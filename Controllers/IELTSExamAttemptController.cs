using Exaln.Constants;
using Exaln.Interfaces;
using Exaln.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Controllers
{
    [Route("ielts-attempt/[controller]")]
    [ApiController]
    public class IELTSExamAttemptController : ControllerBase
    {
        private readonly IIELTSExamAttemptRepository _ieltsExamAttemptRepository;
        private readonly IRedisService _redis;

        public IELTSExamAttemptController(IIELTSExamAttemptRepository ieltsExamAttemptRepository, IRedisService redis)
        {
            _ieltsExamAttemptRepository = ieltsExamAttemptRepository;
            _redis = redis;
        }

        [HttpPost("start-reading-exam/{examID}")]
        //[Authorize]
        public async Task<IActionResult> StartOrResumeReadingExam(int examID, bool isTimed)
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

            var examAttemptID = await _ieltsExamAttemptRepository.GetOrStartExamAttemptAsync(examID,userId);

            var examAttemptModule = await _ieltsExamAttemptRepository.GetOrStartExamAttemptModuleAsync(examAttemptID, ExamAttempModuleType.Reading, isTimed);

            var sectionList = await _ieltsExamAttemptRepository.GetReadingQuestions(examID, examAttemptID, examAttemptModule.ExamAttemptModuleID, examAttemptModule.remainingSeconds == ExamValues.readingExamSeconds);

            return Ok(sectionList);
        }


    }
}
