using Exaln.DBContext;
using Exaln.Interfaces;
using Exaln.Models;

namespace Exaln.Repository
{
    public class IELTSReadingRepository : IIELTSReadingRepository
    {
        private readonly ApplicationDbContext _context;

        public IELTSReadingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<IELTSExam?> GetExamByIdAsync(int examId)
        {
            return await _context.IELTSExams.FindAsync(examId).AsTask();
        }

    }
}
