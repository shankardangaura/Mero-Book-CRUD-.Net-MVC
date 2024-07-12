using MeroBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeroBook.DataAccess.Repository.IRepository
{
    public interface IAuthenticationRepository : IRepository<Authentication>
    {
        void Update(Authentication obj);
    }
}
