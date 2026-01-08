namespace Exaln.DTOs.IELTSDTO
{
    public class SaveReadingQuestionAnswerDTO
    {
        public Guid ExamAttemptModuleID { get; set; }
        public int QuestionID { get; set; }
        public string? UsersAnswer { get; set; }
    }
}
