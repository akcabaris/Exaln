using System.ComponentModel.DataAnnotations;

namespace Exaln.DTOs.IELTSDTO
{
    public class IELTSReadingSectionPartDTO
    {
        public int ReadingSectionPartID { get; set; }

        public short? QuestionTypeEnumID { get; set; }

        public short? PartNo { get; set; }

        [MaxLength(500)]
        public string? SectionPartExplanation { get; set; }

        public List<IELTSReadingQuestionDTO> QuestionList { get; set; } = new List<IELTSReadingQuestionDTO>(); 
    }
}
