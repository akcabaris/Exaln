using Exaln.DBContext;
using Exaln.DTOs.IELTSDTO;
using Exaln.Interfaces;
using Exaln.Constants.Enums;

using Microsoft.EntityFrameworkCore;

namespace Exaln.Repository
{
    public class IELTSReadingRepository : IIELTSReadingRepository
    {
        private readonly ApplicationDbContext _context;

        public IELTSReadingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IELTSReadingPracticeDTO>> GetReadingPracticeListAsync(string userID, IELTSEnum.ExamType examType)
        {
            var examList = await _context.IELTSExams
                .Where(x => x.ExamTypeEnumID == (short)examType)
                .Select(x =>
                    new IELTSReadingPracticeDTO
                    {
                        ExamID = x.ExamID
                    })
                .ToListAsync();


            return examList;
        }
    }
}
