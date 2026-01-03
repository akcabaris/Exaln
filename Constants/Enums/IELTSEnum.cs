namespace Exaln.Constants.Enums
{
    public static class IELTSEnum
    {
        public enum ExamType : short
        {
            AcademicFull = 1,
            GeneralTrainingFull = 2,
            AcademicPractice = 3,
            GeneralTrainingPractice = 4,
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

        public enum ExamStatus : short
        {
            NotStarted = 0,
            InProgress = 1,
            Completed = 2,
        }

        public enum ExamAttempModuleType : short
        {
            Listening = 1,
            Reading = 2,
            Writing = 3,
            Speaking = 4
        }

        public enum ExamAttemptModuleStatus : short
        {
            NotStarted = 0,
            InProgress = 1,
            Completed = 2,
        }
    }
}
