using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Models
{
    [Table("ReadingQuestion", Schema = "IELTS")]
    public class IELTSReadingQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingQuestionID { get; set; }

        public short? QuestionNo { get; set; }

        [MaxLength(1000)]
        public string? QuestionText { get; set; }

        [MaxLength(200)]
        public string? Answer { get; set; }

        public int? ReadingSectionPartID { get; set; }

        [ForeignKey("ReadingSectionPartID")]
        public IELTSReadingSectionPart? ReadingSectionPart { get; set; }
    }
}