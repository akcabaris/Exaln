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

    }
}
