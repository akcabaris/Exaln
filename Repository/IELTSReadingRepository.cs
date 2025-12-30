using Exaln.DBContext;
using Exaln.DTOs.IELTSDTO;
using Exaln.Interfaces;
using Exaln.Models;
using Microsoft.EntityFrameworkCore;

namespace Exaln.Repository
{
    public class IELTSReadingRepository : IIELTSReadingRepository
    {
        private readonly ApplicationDbContext _context;

        public IELTSReadingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<IELTSReadingSectionDTO>> GetReadingSectionListAsync(int examID)
        {
            var readingSectionList = await _context.IELTSReadingSections
                .AsNoTracking()
                .Where(r => r.ExamID == examID)
                .Select(r =>
                    new IELTSReadingSectionDTO
                    {
                        ExamID = r.ExamID,
                        PassageHeader = r.PassageHeader,
                        PassageText = r.PassageText,
                        ReadingSectionID = r.ReadingSectionID,
                        SectionExplanation = r.SectionExplanation,
                        SectionNo = r.SectionNo
                    }
                )
                .ToListAsync();

            return readingSectionList;
        }

        public async Task<List<IELTSReadingSectionPartDTO>> GetReadingSectionPartListAsync(int readingSectionID)
        {
            var readingSectionPartList = await _context.IELTSReadingSectionParts
                .AsNoTracking()
                .Where(r => r.ReadingSectionID == readingSectionID)
                .Select(
                    r => new IELTSReadingSectionPartDTO
                    {
                        PartNo = r.PartNo,
                        ReadingSectionID = r.ReadingSectionID,
                        QuestionTypeEnumID = r.QuestionTypeEnumID,
                        ReadingSectionPartID = r.ReadingSectionPartID,
                        SectionPartExplanation = r.SectionPartExplanation,
                        QuestionList = r.ReadingQuestions.Select(
                            q => new IELTSReadingQuestionDTO
                            {
                                ReadingQuestionID = q.ReadingQuestionID,
                                QuestionNo  = q.QuestionNo,
                                QuestionText = q.QuestionText,
                            }
                        ).ToList()
                    }
                )
                .ToListAsync();


            return readingSectionPartList;
        }
    }
}
