using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Models
{
    [Table("ReadingSectionPart", Schema = "IELTS")]
    public class IELTSReadingSectionPart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReadingSectionPartID { get; set; }

        public int? ReadingSectionID { get; set; }

        public short? QuestionTypeEnumID { get; set; }

        public short? PartNo { get; set; }

        [ForeignKey("ReadingSectionID")]
        public IELTSReadingSection? ReadingSection { get; set; }

        public ICollection<IELTSReadingQuestion> ReadingQuestions { get; set; } = new List<IELTSReadingQuestion>();
    }
}