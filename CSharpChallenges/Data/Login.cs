using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Data
{
    public class Login
    {
        private readonly AppDbContext _db;
        public Login(AppDbContext db)
        {
            _db = db;
        }
        public bool LoginUser( string firstName, string inputPin)
        {
            var user = _db.TblUser
                .FirstOrDefault(u => u.FirstName == firstName);

            if (user == null)
                return false;

            string inputHash = GetMd5(inputPin);

            return user.Pin == inputHash;
        }

        public string GetMd5(string input)
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
