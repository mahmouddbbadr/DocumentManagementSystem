using Application.IRepository;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentManagementSystem.Domain.IRepository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        ICollection<Document> SortByName(string userId);
        ICollection<Document> SortByDate(string userId);
        ICollection<Document> SortBySize(string userId);
    }
}
