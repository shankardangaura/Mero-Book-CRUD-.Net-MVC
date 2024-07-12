using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeroBook.DataAccess.Repository.IRepository
{
    public interface IMeroBookRepository
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }
        IAuthenticationRepository Authentication { get; }
        void Save();
    }
}
