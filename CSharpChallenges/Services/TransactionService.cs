using CSharpChallenges.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpChallenges.Services
{
    public class TransactionService
    {

        private AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }
    }
}
