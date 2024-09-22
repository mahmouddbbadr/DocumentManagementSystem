using Application.IRepository;
using Domain.Models;
using Infrasturcture.Data;




namespace Infrasturcture.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext context;

        public TagRepository(DataContext context)
        {
            this.context = context;
        }

        public Tag Get(string name)
        {
            return context.Tags.FirstOrDefault(t=> t.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public bool Create(Tag entity)
        {
            context.Tags.Add(entity);
            return Save();
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0? true: false;
        }

    }
}
