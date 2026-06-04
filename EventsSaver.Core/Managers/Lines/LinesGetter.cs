using EventsSaver.Core.Repositories.Line;
using EventsSaver.Shared.Entities;
using EventsSaver.Shared.Managers.Lines;

namespace EventsSaver.Core.Managers.Lines;

internal class LinesGetter : ILinesGetter
{
    private readonly ILinesReadRepository _linesReadRepository;

    public LinesGetter(ILinesReadRepository linesReadRepository)
    {
        _linesReadRepository = linesReadRepository ?? throw new ArgumentNullException(nameof(linesReadRepository));
    }

    public async Task<IEnumerable<ILineEntity>> GetLinesPaged(int pageNumber, int pageSize) =>
        await _linesReadRepository.GetLinesPaged(pageNumber, pageSize);

    public async Task<int> GetLinesCount() => await _linesReadRepository.GetLinesCount();
}