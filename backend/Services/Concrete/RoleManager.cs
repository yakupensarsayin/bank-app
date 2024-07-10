using backend.Models;
using backend.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace backend.Services.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly BankDbContext _context;

        public RoleManager(BankDbContext context)
        {
            _context = context;
        }
        public async Task<Role> GetRole(Expression<Func<Role, bool>> filter)
        {
            /*
             * Since our project is very small and the number of roles are low,
             * it was disabled with the confidence that we would not make mistakes.
             */

#pragma warning disable CS8603
            return await _context.Roles.Where(filter).FirstOrDefaultAsync();
#pragma warning restore CS8603
        }
    }
}
