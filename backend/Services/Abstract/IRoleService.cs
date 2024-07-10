using backend.Models;
using System.Linq.Expressions;

namespace backend.Services.Abstract
{
    public interface IRoleService
    {
        public Task<Role> GetRole(Expression<Func<Role, bool>> filter);
    }
}
