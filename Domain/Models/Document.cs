using System.ComponentModel.DataAnnotations.Schema;




namespace Domain.Models
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WWWRootName { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime UploadedAt { get; set; }
        public string extention { get; set; }
        public string Owner { get; set; }
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<Tag> Tags { get; set; }

        [ForeignKey("Directory")]
        public Guid DirectoryId { get; set; }
        public virtual Directory Directory { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public virtual AppUser AppUser { get; set; }
    }
}
