using Exaln.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IELTSExamController : Controller
    {
        private readonly IIELTSExamRepository _ieltsExamRepository;

        public IELTSExamController(IIELTSExamRepository ieltsExamRepository)
        {
            _ieltsExamRepository = ieltsExamRepository;
        }

        [HttpGet("exams/{examType}")]
        //[Authorize]
        public async Task<IActionResult> GetExams(ExamType examType)
        {
            var result = await _ieltsExamRepository.GetExamList(examType);

            return Ok(result);
        }


    }
}
