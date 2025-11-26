using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Exaln.Models
{
    [Table("Exam", Schema = "IELTS")]
    public class IELTSExam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamID { get; set; }

        public short? ExamTypeEnumID { get; set; }

        public DateTimeOffset? CreatedAt { get; set; } 
        
        public DateTimeOffset? UpdatedAt { get; set; } 

        public ICollection<IELTSReadingSection> ReadingSections { get; set; } = new List<IELTSReadingSection>();
    }
}