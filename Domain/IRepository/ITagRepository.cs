using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
    public interface ITagRepository
    {
        Tag Get(string name);
        bool Create(Tag entity);
        bool Save();
    }
}
