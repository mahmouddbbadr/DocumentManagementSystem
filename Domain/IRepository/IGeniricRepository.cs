using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll(string userId);
        T GetByIdOrName(Guid id, string userId);
        T GetByIdOrName(string name, string userId);
        bool CheckEntityExits(string name, string userId);
        bool CheckEntityExits(Guid id, string userId);
        bool CheckEntityExits(string name);
        bool Create(T entity);
        bool update(T entity);
        bool Delete(string name, string userId);
        bool Delete(Guid id, string userId);
        bool Save();
    }
}
