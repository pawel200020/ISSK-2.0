using EventsSaver.Core.Repositories.Line;
using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Managers.Lines;

public class LinesCreator
{
        private readonly ILinesWriteRepository _linesWriteRepository;
    
        public LinesCreator(ILinesWriteRepository linesWriteRepository)
        {
            _linesWriteRepository = linesWriteRepository ?? throw new ArgumentNullException(nameof(linesWriteRepository));
        }
    
        public async Task<bool> CreateLine(ILineEntity line) =>
            await _linesWriteRepository.CreateLine((LineEntity)line);
}