using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Entities
{
    [Table("ExamAttemptModule", Schema = "IELTS")]
    public class IELTSExamAttemptModule
    {
        public Guid ExamAttemptModuleID { get; set; }

        public Guid? ExamAttemptID { get; set; }

        public short? ExamAttempModuleTypeID { get; set; }

        public int? RawScore { get; set; }

        public decimal? BandScore { get; set; }

        public string? ProgressJson { get; set; }

        public int? remainingSeconds { get; set;  }

        public short? ExamAttemptModuleStatusEnumID { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public IELTSExamAttempt? ExamAttempt { get; set; }
        public ICollection<IELTSExamAttemptReadingAnswer> ReadingAnswers { get; set; } = new List<IELTSExamAttemptReadingAnswer>();
    }
}
