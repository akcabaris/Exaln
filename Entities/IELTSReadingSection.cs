using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Models
{
    [Table("ReadingSection", Schema = "IELTS")]
    public class IELTSReadingSection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingSectionID { get; set; }

        public int? ExamID { get; set; }

        public short? SectionNo { get; set; }

        [MaxLength(1200)]
        public string? PassageHeader { get; set; }

        [MaxLength(10000)]
        public string? PassageText { get; set; }

        [MaxLength(500)]
        public string? SectionExplanation { get; set; }

        [ForeignKey("ExamID")]
        public IELTSExam? Exam { get; set; }

        public ICollection<IELTSReadingSectionPart> ReadingSectionParts { get; set; } = new List<IELTSReadingSectionPart>();
    }
}