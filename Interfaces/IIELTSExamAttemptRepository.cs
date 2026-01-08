using Exaln.Constants.Enums;
using Exaln.DTOs.IELTSDTO;
using Exaln.Entities;

namespace Exaln.Interfaces
{
    public interface IIELTSExamAttemptRepository
    {
        Task<Guid> GetOrStartExamAttemptAsync(int examID, string userID);
        Task<IELTSExamAttemptModule> GetOrStartExamAttemptModuleAsync(Guid examAttemptID, IELTSEnum.ExamAttempModuleType moduleType, bool isTimed);
        Task<List<IELTSReadingSectionDTO>> GetReadingQuestionsAsync(int examID, Guid examAttemptID, Guid examAttemptModuleID, bool isExamAttemptNew);
        Task SaveReadingQuestionUsersAnswerAsync(Guid examAttemptModuleID, string userID, int readingQuestionID, string? answer);
    }
}
