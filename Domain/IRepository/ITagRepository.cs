using Domain.Models;




namespace Application.IRepository
{
    public interface ITagRepository
    {
        Tag Get(string name);
        bool Create(Tag entity);
        bool Save();
    }
}
