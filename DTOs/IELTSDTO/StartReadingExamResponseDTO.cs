namespace Exaln.DTOs.IELTSDTO
{
    public class StartReadingExamResponseDTO
    {
        public Guid ExamAttemptModuleID { get; set; }
        public List<IELTSReadingSectionDTO> Sections { get; set; } = new List<IELTSReadingSectionDTO>();
    }
}
