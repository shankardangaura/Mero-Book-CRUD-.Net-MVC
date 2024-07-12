using MeroBook.DataAccess.Data;
using MeroBook.DataAccess.Repository.IRepository;
using MeroBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeroBook.DataAccess.Repository
{
    public class AuthenticationRepository : Repository<Authentication>, IAuthenticationRepository
    {
        private ApplicationDbContext _db;
        public AuthenticationRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Authentication obj)
        {
            _db.Authentications.Update(obj);
        }
    }
}
