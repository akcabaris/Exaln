using Exaln.DTOs.IELTSDTO;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Interfaces
{
    public interface IIELTSExamRepository
    {
        public Task<List<IELTSExamDTO>> GetExamList(ExamType examTypeID);
    }
}
