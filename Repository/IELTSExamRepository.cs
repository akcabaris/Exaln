using Exaln.DBContext;
using Exaln.DTOs.IELTSDTO;
using Exaln.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Repository
{
    public class IELTSExamRepository : IIELTSExamRepository
    {
        private readonly ApplicationDbContext _context;

        public IELTSExamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IELTSExamDTO>> GetExamList(ExamType examTypeEnumID)
        {
            var examList = await _context.IELTSExams
                .AsNoTracking()
                .Where(e => e.ExamTypeEnumID == (short)examTypeEnumID)
                .Select(e => new IELTSExamDTO
                {
                    ExamID = e.ExamID,
                    ExamTypeEnumID = e.ExamTypeEnumID,
                }).ToListAsync();

            return examList;
        }
    }
}
