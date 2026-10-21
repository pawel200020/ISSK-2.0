namespace Authentication.Application.Encryption;

internal interface IEncryptionManager
{
    /// <summary>
    /// Encrypts plaintext using AES-256 with CBC mode and random IV.
    /// IV is prepended to the ciphertext for storage.
    /// </summary>
    string Encrypt(string plaintext, string key);

    /// <summary>
    /// Decrypts ciphertext that was encrypted with Encrypt method.
    /// Expects IV to be prepended to the ciphertext.
    /// </summary>
    string Decrypt(string ciphertext, string key);
}