using Application.IRepository;
using Domain.Models;




namespace DocumentManagementSystem.Domain.IRepository
{
    public interface IDocumentRepository : IGenericRepository<Document>
    {
        bool CheckDocumentExits(string name, string userId, string directoryName);
        ICollection<Document> GetAllShared();
        ICollection<Document> GetByDirectoryId(Guid directoryId);
        ICollection<Document> SortByNameAscending(Guid directoryId, string userId);
        ICollection<Document> SortByDateAscending(Guid directoryId, string userId);
        ICollection<Document> SortBySizeAscending(Guid directoryId, string userId);
        ICollection<Document> SortByNameDescending(Guid directoryId, string userId);
        ICollection<Document> SortByDateDescending(Guid directoryId, string userId);
        ICollection<Document> SortBySizeDescending(Guid directoryId, string userId);

         ICollection<Document> GetAllSharedSortedByNameAscending();
         ICollection<Document> GetAllSharedSortedByNameDescending();
         ICollection<Document> GetAllSharedSortedBySizeAscending();
         ICollection<Document> GetAllSharedSortedBySizeDescending();
         ICollection<Document> GetAllSharedSortedByDeteAscending();
         ICollection<Document> GetAllSharedSortedByDateDescending();

         ICollection<Document> SearchAllShared(string filter);
         ICollection<Document> AdminSearch(string filter, Guid directoryId);





    }
}
