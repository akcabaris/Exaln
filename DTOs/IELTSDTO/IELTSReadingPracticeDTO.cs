using Exaln.Constants.Enums;

namespace Exaln.DTOs.IELTSDTO
{
    public class IELTSReadingPracticeDTO
    {
        public int ExamID { get; set; }
        public IELTSEnum.ExamAttemptModuleStatus Status { get; set; }

        public TimeSpan RemainingTime { get; set; }
    }
}
