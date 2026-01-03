using Exaln.Constants.Enums;
using Exaln.DTOs.IELTSDTO;
using Exaln.Models;
using static Exaln.Constants.Enums.IELTSEnum;

namespace Exaln.Interfaces
{
    public interface IIELTSReadingRepository
    {
        Task<List<IELTSReadingPracticeDTO>> GetReadingPracticeListAsync(string userID, IELTSEnum.ExamType examType);
    }
}
