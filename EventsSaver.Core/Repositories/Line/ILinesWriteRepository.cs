using EventsSaver.Shared.Entities;

namespace EventsSaver.Core.Repositories.Line;

public interface ILinesWriteRepository
{
    Task<bool> CreateLine(LineEntity line);
}