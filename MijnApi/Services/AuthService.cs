using MijnApi.Data;
using MijnApi.Helpers;
using MijnApi.Models;
using System.Linq;

namespace MijnApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        public AuthService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthResult Register(User user)
        {
            if (_context.Users.Any(u => u.Username == user.Username))
            {
                return new AuthResult { Success = false, Message = "Gebruikersnaam bestaat al." };
            }

            if (!PasswordHelper.ValidatePassword(user.Password))
            {
                return new AuthResult { Success = false, Message = "Wachtwoord voldoet niet aan de vereisten." };
            }

            user.Password = PasswordHelper.HashPassword(user.Password);
            _context.Users.Add(user);
            _context.SaveChanges();

            return new AuthResult { Success = true, Message = "Registratie succesvol." };
        }

        public AuthResult Login(User user)
        {
            var existingUser = _context.Users.SingleOrDefault(u => u.Username == user.Username);
            if (existingUser == null || existingUser.Password != PasswordHelper.HashPassword(user.Password))
            {
                return new AuthResult { Success = false, Message = "Ongeldige gebruikersnaam of wachtwoord." };
            }

            // Hier kun je sessiebeheer logica toevoegen

            return new AuthResult { Success = true, Message = "Inloggen succesvol." };
        }
    }
}