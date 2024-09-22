using Application.IRepository;
using Domain.Models;




namespace DocumentManagementSystem.Domain.IRepository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        bool CheckDocumentExits(string name, string userId, string directoryName);
        ICollection<Document> GetAllShared();
        ICollection<Document> GetByDirectoryId(Guid directoryId);
        ICollection<Document> SortByName(string userId);
        ICollection<Document> SortByDate(string userId);
        ICollection<Document> SortBySize(string userId);
    }
}
