using Exaln.DBContext;
using Exaln.DTOs.IELTSDTO;
using Exaln.Interfaces;
using Exaln.Models;
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

        public async Task<List<IELTSReadingSectionDTO>> GetIELTSExamReadingSectionListAsync(int examID)
        {
            var readingSectionList = await _context.IELTSReadingSections
                .AsNoTracking()
                .Where(r => r.ExamID == examID)
                .Select(r =>
                    new IELTSReadingSectionDTO
                    {
                        ReadingSectionID = r.ReadingSectionID,
                        ExamID = examID,
                        SectionNo = r.SectionNo,
                        PassageHeader = r.PassageHeader,
                        PassageText = r.PassageText,
                        SectionExplanation = r.SectionExplanation,
                    }
                )
                .ToListAsync();

            return readingSectionList;
        }
    }
}
