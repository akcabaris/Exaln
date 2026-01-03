using Exaln.Constants.Enums;
using Exaln.Entities;

namespace Exaln.Interfaces
{
    public interface IIELTSExamAttemptRepository
    {
        Task<Guid> GetOrStartExamAttemptIDAsync(int examID, string userID);
        Task<IELTSExamAttemptModule> GetOrStartExamAttemptModuleAsync(Guid examAttemptID, IELTSEnum.ExamAttempModuleType moduleType, bool isTimed);

        Task<string> GetReadingQuestions(int examId,Guid examAttempID);
    }
}
