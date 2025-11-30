namespace Exaln.Constants.Enums
{
    public static class IELTSEnum
    {
        public enum ExamType : short
        {
            Academic = 1,
            General_Training = 2,
        }
        public enum ReadingQuestionType : short
        {
            MultipleChoice = 1,
            TrueFalseNotGiven = 2,
            YesNoNotGiven = 3,
            MatchingHeadings = 4,
            MatchingInformation = 5,
            MatchingFeatures = 6,
            MatchingSentenceEndings = 7,
            SentenceCompletion = 8,
            SummaryCompletion = 9,
            NoteCompletion = 10,
            TableCompletion = 11,
            FlowChartCompletion = 12,
            DiagramLabelCompletion = 13,
            ShortAnswerQuestions = 14
        }
    }
}
