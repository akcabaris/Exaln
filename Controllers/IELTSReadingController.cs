using Exaln.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IELTSReadingController : Controller
    {
        private readonly IIELTSReadingRepository _ieltsReadingRepository;

        public IELTSReadingController(IIELTSReadingRepository ieltsReadingRepository)
        {
            _ieltsReadingRepository = ieltsReadingRepository;
        }

        [HttpGet("readingSections/{examID}")]
        [Authorize]
        public async Task<IActionResult> GetReadingSections(int examID)
        {
            var result = await _ieltsReadingRepository.GetIELTSExamReadingSectionListAsync(examID);

            return Ok(result);
        }

    }
}
