using Exaln.DBContext;
using Exaln.Entities;
using Exaln.Interfaces;
using Microsoft.EntityFrameworkCore;
using Exaln.Constants.Enums;

using Exaln.Constants;
using Exaln.DTOs.IELTSDTO;

namespace Exaln.Repository
{
    public class IELTSExamAttemptRepository : IIELTSExamAttemptRepository
    {
        private ApplicationDbContext _context;

        public IELTSExamAttemptRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Guid> GetOrStartExamAttemptAsync(int examID, string userID)
        {
            var examAttempID = await _context.IELTSExamAttempts.
                Where(e => e.ExamID == examID && e.UserID == userID)
                .Select(e => e.ExamAttemptID).FirstOrDefaultAsync();

            if (examAttempID == Guid.Empty)
            {
                var newExamAttempt = new IELTSExamAttempt
                {
                    UserID = userID,
                    ExamID = examID,
                    ExamStatusEnumID = (short)IELTSEnum.ExamStatus.InProgress,
                    CreatedAt = DateTime.UtcNow,

                };

                await _context.AddAsync(newExamAttempt);
                await _context.SaveChangesAsync();

                examAttempID = newExamAttempt.ExamAttemptID;
            }

            return examAttempID;
        }

        public async Task<IELTSExamAttemptModule> GetOrStartExamAttemptModuleAsync(Guid examAttemptID, IELTSEnum.ExamAttempModuleType moduleType, bool isTimed)
        {
            var examAttemptModule = await _context.IELTSExamAttemptModules
                .Where(e => e.ExamAttemptID == examAttemptID && e.ExamAttempModuleTypeID == (short)moduleType)
                .FirstOrDefaultAsync();

            if (examAttemptModule == null)
            {
                examAttemptModule = new IELTSExamAttemptModule
                {
                    ExamAttemptID = examAttemptID,
                    CreatedAt = DateTime.UtcNow,
                    ExamAttempModuleTypeID = (short)moduleType,
                    remainingSeconds = isTimed ? ExamValues.readingExamSeconds : null,
                    ExamAttemptModuleStatusEnumID = (short)IELTSEnum.ExamAttemptModuleStatus.InProgress,
                };

                await _context.IELTSExamAttemptModules.AddAsync(examAttemptModule);
                await _context.SaveChangesAsync();
            }

            return examAttemptModule;
        }

        public async Task<List<IELTSReadingSectionDTO>> GetReadingQuestions(int examID, Guid examAttemptID, Guid examAttemptModuleID, bool isExamAttemptNew)
        {

            var result = await _context.IELTSReadingSections
                .Where(s => s.ExamID == examID)
                .OrderBy(s => s.SectionNo)
                .Select(s => new IELTSReadingSectionDTO
                {
                    ReadingSectionID = s.ReadingSectionID,
                    SectionNo = s.SectionNo,
                    ExamID = examID,
                    PassageHeader = s.PassageHeader,
                    PassageText = s.PassageText,
                    SectionExplanation = s.SectionExplanation,
                    SectionParts = s.ReadingSectionParts
                                    .OrderBy(p => p.PartNo)
                                    .Select(p => new IELTSReadingSectionPartDTO
                                    {
                                        PartNo = p.PartNo,
                                        QuestionTypeEnumID = p.QuestionTypeEnumID,
                                        SectionPartExplanation = p.SectionPartExplanation,
                                        ReadingSectionPartID = p.ReadingSectionPartID,
                                        QuestionList = isExamAttemptNew ?
                                                                (
                                                                    from q in p.ReadingQuestions
                                                                    join a in _context.IELTSExamAttemptReadingAnswers
                                                                                        .Where(x => x.ExamAttemptModuleID == examAttemptModuleID)
                                                                                        on q.ReadingQuestionID equals a.ReadingQuestionID into gj
                                                                    from a in gj.DefaultIfEmpty()
                                                                    orderby q.QuestionNo
                                                                    select new IELTSReadingQuestionDTO
                                                                    {
                                                                        ExamAttemptReadingAnswerID = a.ExamAttemptReadingAnswerID,
                                                                        QuestionNo = q.QuestionNo,
                                                                        QuestionText = q.QuestionText,
                                                                        ReadingQuestionID = q.ReadingQuestionID,
                                                                        UsersAnswer = q.Answer
                                                                    }
                                                                ).ToList()
                                                            :
                                                                p.ReadingQuestions.OrderBy(pq => pq.QuestionNo)
                                                                .Select(pq => new IELTSReadingQuestionDTO
                                                                {
                                                                    ReadingQuestionID = pq.ReadingQuestionID,
                                                                    QuestionNo = pq.QuestionNo,
                                                                    QuestionText = pq.QuestionText,

                                                                }).ToList(),

                                    })
                                    .ToList()
                })
                .ToListAsync();


            return result;
        }
    }
}
