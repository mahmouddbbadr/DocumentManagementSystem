


namespace Domain.Models
{
    public class WorkSpace
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public virtual ICollection<Directory> Directories { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}

