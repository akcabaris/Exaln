using System.ComponentModel.DataAnnotations.Schema;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Entities
{
    [Table("ExamAttempt", Schema = "IELTS")]
    public class IELTSExamAttempt
    {
        public Guid ExamAttemptID {  get; set; }
        public int ExamID { get; set; }
        public string? UserID { get; set; }
        public short? ExamStatusEnumID { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? SubmittedAt { get; set; }

        public List<IELTSExamAttemptModule> Modules { get; set; } = new List<IELTSExamAttemptModule>();

    }
}
