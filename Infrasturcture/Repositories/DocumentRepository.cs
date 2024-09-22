using DocumentManagementSystem.Domain.IRepository;
using Domain.Models;
using Infrasturcture.Data;


namespace Infrasturcture.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly DataContext context;

        public DocumentRepository(DataContext context)
        {
            this.context = context;
        }
        public bool CheckEntityExits(string name, string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).Any(d => d.Name.Trim().ToLower() == name.Trim().ToLower());
        } 
        
        public bool CheckDocumentExits(string name, string userId, string directoryName)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId && d.Directory.Name == directoryName).Any(d => d.Name.Trim().ToLower() == name.Trim().ToLower());
        }
        public bool CheckEntityExits(Guid id, string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).Any(d => d.Id == id);
        }

        public bool CheckEntityExits(string name)
        {
            return context.Documents.Where(d => d.IsDeleted == false).Any(d => d.Name == name);
        }


        public ICollection<Document> GetAll(string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).ToList();
        }

        public Document GetByIdOrName(Guid id, string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d => d.Id == id);
        }

        public Document GetByIdOrName(string name, string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d => d.Name.Trim().ToLower() == name.Trim().ToLower());

        }

        public bool Create(Document entity)
        {
            context.Add(entity);
            return Save();
        }


        public bool update(Document entity)
        {
            context.Update(entity);
            return Save();
        }

        public bool Delete(string name, string userId)
        {
            var document = context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d => d.Name.Trim().ToLower() == name.Trim().ToLower());
            document.IsDeleted = true;
            return Save();
        }
        public bool Delete(Guid id, string userId)
        {
            var document = context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d => d.Id == id);
            document.IsDeleted = true;
            return Save();
        }

        

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0? true : false;
        }

        public ICollection<Document> SortByName(string userId)
        {
            return context.Documents.Where(d=> d.IsDeleted == false && d.UserId == userId).OrderBy(d => d.Name).ToList();
        }

        public ICollection<Document> SortByDate(string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).OrderBy(d => d.UploadedAt).ToList();
        }

        public ICollection<Document> SortBySize(string userId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.UserId == userId).OrderBy(d => d.Size).ToList();
        }

        public ICollection<Document> GetByDirectoryId(Guid directoryId)
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.DirectoryId == directoryId).ToList();
        }

        public ICollection<Document> GetAllShared()
        {
            return context.Documents.Where(d => d.IsDeleted == false && d.Directory.IsPrivate == false).ToList();

        }
    }
}
