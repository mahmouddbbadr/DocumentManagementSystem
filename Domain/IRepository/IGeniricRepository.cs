using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        ICollection<T> GetAll(string usrId);
        T GetByIdOrName(Guid id, string usrId);
        T GetByIdOrName(string name, string usrId);
        bool CheckEntityExits(string name, string usrId);
        bool CheckEntityExits(Guid id, string usrId);
        bool CheckEntityExits(string name);

        bool Create(T entity);
        bool update(T entity);
        bool Delete(string name, string usrId);
        bool Delete(Guid id, string usrId);
        bool Save();
    }
}
