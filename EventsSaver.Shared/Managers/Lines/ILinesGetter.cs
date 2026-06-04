using EventsSaver.Shared.Entities;

namespace EventsSaver.Shared.Managers.Lines;

public interface ILinesGetter
{
    Task<IEnumerable<ILineEntity>> GetLinesPaged(int pageNumber, int pageSize);
    Task<int> GetLinesCount();
}