namespace Users.Core.Repositories.Read;

internal interface IReadTwoFactorAuthUserRepository
{
    Task<bool> VerifyTwoFactorTokenAsync(string verificationCode, Guid userId);
}