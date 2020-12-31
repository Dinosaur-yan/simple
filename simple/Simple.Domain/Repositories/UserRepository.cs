using Simple.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Simple.Domain.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context)
            : base(context)
        {
        }
    }

    public interface IUserRepository : IRepository<User>
    {
    }
}
