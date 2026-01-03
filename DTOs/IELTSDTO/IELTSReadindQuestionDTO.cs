using System.ComponentModel.DataAnnotations;

namespace Exaln.DTOs.IELTSDTO
{
    public class IELTSReadingQuestionDTO
    {
        public int ReadingQuestionID { get; set; }

        public short? QuestionNo { get; set; }

        [MaxLength(1000)]
        public string? QuestionText { get; set; }
        public string? UsersAnswer { get; set; }

        public long ExamAttemptReadingAnswerID { get; set; }
    }
}
