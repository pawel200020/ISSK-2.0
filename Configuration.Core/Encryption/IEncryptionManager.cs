namespace Configuration.Encryption;

internal interface IEncryptionManager
{
    /// <summary>
    /// Encrypts plaintext using AES-256 with CBC mode and random IV.
    /// IV is prepended to the ciphertext for storage.
    /// </summary>
    /// <param name="plaintext">The plaintext to encrypt.</param>
    /// <param name="key">The encryption key (passphrase).</param>
    /// <returns>The encrypted string (Base64) with IV prepended.</returns>
    string Encrypt(string plaintext, string key);

    /// <summary>
    /// Decrypts ciphertext that was encrypted with Encrypt method.
    /// Expects IV to be prepended to the ciphertext.
    /// </summary>
    /// <param name="ciphertext">The ciphertext to decrypt (Base64 encoded with IV prepended).</param>
    /// <param name="key">The encryption key (passphrase).</param>
    /// <returns>The decrypted plaintext string.</returns>
    string Decrypt(string ciphertext, string key);
}