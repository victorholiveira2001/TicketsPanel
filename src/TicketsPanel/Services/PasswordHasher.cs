using Isopoh.Cryptography.Argon2;
using TicketsPanel.Models;

namespace TicketsPanel.Services{
    public class PasswordHasher{
        public string SetPassword(string password)
        {
            return Argon2.Hash(password);
        }
        public bool VerifyPassword(string password)
        {
            var user = new User();


            return Argon2.Verify(password, user.Password);
        }
    }
}

