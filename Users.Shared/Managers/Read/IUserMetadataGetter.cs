using Users.Shared.Models;

namespace Users.Shared.Managers.Read;

public interface IUserMetadataGetter
{
    Task<ApplicationUserMetadata> GetUserMetadataAsync(Guid userId);
}