using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Repositories.Line;

public interface ILinesReadRepository
{
    Task<IEnumerable<ILineEntity>> GetLinesPaged(int page, int pageSize);
    Task<int> GetLinesCount();
}