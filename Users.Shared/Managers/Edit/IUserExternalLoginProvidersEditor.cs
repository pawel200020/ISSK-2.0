namespace Users.Shared.Managers.Edit;

public interface IUserExternalLoginProvidersEditor
{
    Task<bool> RemoveAssignedExternalLogin(Guid userId, string provider, string providerKey);
    Task<(bool,bool)> AddAssignedExternalLogin(Guid userId);
}