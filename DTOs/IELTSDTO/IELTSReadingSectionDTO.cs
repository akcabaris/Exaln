using System.ComponentModel.DataAnnotations;

namespace Exaln.DTOs.IELTSDTO
{
    public class IELTSReadingSectionDTO
    {
        public int ReadingSectionID { get; set; }

        public int? ExamID { get; set; }

        public short? SectionNo { get; set; }

        [MaxLength(1200)]
        public string? PassageHeader { get; set; }

        [MaxLength(10000)]
        public string? PassageText { get; set; }

        [MaxLength(500)]
        public string? SectionExplanation { get; set; }

        public List<IELTSReadingSectionPartDTO> SectionParts { get; set; } = new List<IELTSReadingSectionPartDTO>();
    }
}
