using System;
using bCrypt = BCrypt.Net.BCrypt;

namespace AccountManager.Services
{
    public class EncryptionService
    {
        public string Encrypt(string plainPassword)
        {
            var salt = bCrypt.GenerateSalt();
            return bCrypt.HashPassword(plainPassword, salt);
        }

        public bool Validate(string plainPassword, string encryptedPassword)
        {
            return bCrypt.Verify(plainPassword, encryptedPassword);
        }
    }
}
