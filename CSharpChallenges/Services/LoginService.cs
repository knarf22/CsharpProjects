using CSharpChallenges.Data;
using CSharpChallenges.Models;
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
    }
}