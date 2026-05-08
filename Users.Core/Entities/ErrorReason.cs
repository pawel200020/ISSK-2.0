namespace Users.Core.Entities;

public enum ErrorReason
{
    UserNotFound,
    WrongPassword,
    UserAlreadyExists,
    InvalidEmail,

    // Password validation specific reasons (match Identity's PasswordValidator checks)
    PasswordTooShort,
    PasswordRequiresNonAlphanumeric,
    PasswordRequiresDigit,
    PasswordRequiresLower,
    PasswordRequiresUpper,
    PasswordRequiresUniqueChars,

    Unknown
}