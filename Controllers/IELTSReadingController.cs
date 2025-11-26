using Exaln.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    }
}
