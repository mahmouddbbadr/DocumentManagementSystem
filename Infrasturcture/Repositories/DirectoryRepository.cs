using Application.IRepository;
using Domain.Models;
using Infrasturcture.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Repositories
{
    public class DirectoryRepository : IGenericRepository<Domain.Models.Directory>
    {
        private readonly DataContext context;

        public DirectoryRepository(DataContext context)
        {
            this.context = context;
        }
        public bool CheckEntityExits(string name, string userId)
        {
            return context.Directories.Where(d => d.IsDeleted == false & d.UserId == userId).Any(d=> d.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool CheckEntityExits(Guid id, string userId)
        {
            return context.Directories.Where(d => d.IsDeleted == false && d.UserId == userId).Any(d => d.Id == id);
        }

        public bool CheckEntityExits(string name)
        {
            return context.Directories.Where(d => d.IsDeleted == false ).Any(d => d.Name == name);
        }

        public ICollection<Domain.Models.Directory> GetAll(string userId)
        {
            return context.Directories.Where(d => d.IsDeleted == false && d.UserId == userId).Include(d=> d.WorkSpace).Include(d=> d.AppUser).ToList();
        }

        public Domain.Models.Directory GetByIdOrName(Guid id, string userId)
        {
            return context.Directories.Where(d => d.IsDeleted == false && d.UserId == userId).Include(d => d.WorkSpace).Include(d => d.AppUser)
                .FirstOrDefault(d => d.Id == id);
        }

        public Domain.Models.Directory GetByIdOrName(string name, string userId)
        {
            return context.Directories.Where(d => d.IsDeleted == false && d.UserId == userId).Include(d => d.WorkSpace).Include(d => d.AppUser)
                .FirstOrDefault(d => d.Name.Trim().ToLower() == name.Trim().ToLower());

        }

        public bool Create(Domain.Models.Directory entity)
        {
            context.Directories.Add(entity);
            return Save();
        }

        public bool update(Domain.Models.Directory entity)
        {
            context.Update(entity);
            return Save();
        }
        public bool Delete(string name, string userId)
        {
            var directory  = context.Directories.Where(d=> d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d=> d.Name.Trim().ToLower() == name.Trim().ToLower());
            directory.IsDeleted = true;
            return Save();
        }
        public bool Delete(Guid id, string userId)
        {
            var directory = context.Directories.Where(d => d.IsDeleted == false && d.UserId == userId).FirstOrDefault(d => d.Id == id);
            directory.IsDeleted = true;
            return Save();
        }

        public  bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false; 
        }


    }
}
