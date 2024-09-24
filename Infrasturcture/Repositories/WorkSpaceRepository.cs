using Application.IRepository;
using Domain.Models;
using Infrasturcture.Data;




namespace Infrasturcture.Repositories
{
    public class WorkSpaceRepository : IGenericRepository<WorkSpace>
    {
        private readonly DataContext context;

        public WorkSpaceRepository(DataContext context)
        {
            this.context = context;
        }

        public bool CheckEntityExits(string name, string usrId)
        {
            return context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).Any(w => w.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool CheckEntityExits(Guid id, string usrId)
        {
            return context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).Any(w => w.Id == id);
        }

        public bool CheckEntityExits(string name)
        {
            return context.WorkSpaces.Where(d => d.IsDeleted == false).Any(d => d.Name == name);
        }

        public ICollection<WorkSpace> GetAll(string usrId)
        {
            return context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).ToList();
        }

        public WorkSpace GetByIdOrName(Guid id, string usrId)
        {
            return context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).FirstOrDefault(w => w.Id == id);
        }

        public WorkSpace GetByIdOrName(string name, string usrId)
        {
            return context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).FirstOrDefault(ws => ws.Name.Trim().ToLower() == name.Trim().ToLower());

        }

        public bool Create(WorkSpace entity)
        {
            context.Add(entity);
            return Save();
        }

        public bool update(WorkSpace entity)
        {
            context.Update(entity);
            return Save();
        }

        public bool Delete(string name, string usrId)
        {
            var workSpace = context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).FirstOrDefault(w => w.Name.Trim().ToLower() == name.Trim().ToLower());
            workSpace.IsDeleted = true;
            return Save();
        }
        public bool Delete(Guid id, string usrId)
        {
            var workSpace = context.WorkSpaces.Where(w => w.IsDeleted == false && w.AppUser.Id == usrId).FirstOrDefault(w => w.Id == id);
            workSpace.IsDeleted = true;
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public ICollection<WorkSpace> Search(string filter, string userId)
        {
            return context.WorkSpaces.Where(d => !d.IsDeleted && d.Name.ToLower().StartsWith(filter.ToLower())).ToList();
        }
    }
}
