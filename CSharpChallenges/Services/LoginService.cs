using CSharpChallenges.Data;
using CSharpChallenges.Models;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;

namespace CSharpChallenges.Services
{
    public class LoginService
    {
        private readonly AppDbContext _context;

        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        public TblUser? LoginUser(string firstName, string inputPin)
        {
            var user = _context.TblUser
                .FirstOrDefault(u => u.FirstName == firstName);

            if (user == null)
                return null;

            string inputHash = HashPin(inputPin);

            if (user.Pin != inputHash)
                return null;

            return user;
        }

        private string HashPin(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = md5.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (var b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public void ChangePin(int userId)
        {
            Console.Write("Enter your new pin: ");
            string newPin = Console.ReadLine();

            var user = (from u in _context.TblUser
                        where u.UserId == userId
                        select u).FirstOrDefault();

            //var user1=  _context.TblUser.FirstOrDefault(u => u.UserId == userId);
            string newPinString = newPin.ToString();

            string hashedPin = HashPin(newPinString);

            user.Pin = hashedPin;

            _context.SaveChanges();
        }
    }
}