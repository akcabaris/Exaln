using Exaln.Models;

namespace Exaln.Interfaces
{
    public interface IIELTSReadingRepository
    {
        Task<IELTSExam?> GetExamByIdAsync(int examId);
    }
}
