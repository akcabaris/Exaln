using Exaln.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Entities
{
    [Table("ExamAttemptReadingAnswer", Schema = "IELTS")]
    public class IELTSExamAttemptReadingAnswer
    {
        public long ExamAttemptReadingAnswerID { get; set; }

        public Guid ExamAttemptModuleID { get; set; }
        public int ReadingQuestionID { get; set; }

        public string Answer { get; set; } = "";

        public DateTime? AnsweredAt { get; set; }

        public IELTSReadingQuestion? ReadingQuestion { get; set; }
        public IELTSExamAttemptModule? ExamAttemptModule { get; set; }
    }

}
