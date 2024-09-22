


namespace DocumentManagementSystem.Services.Dtos
{
    public class DocumentOutputDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string WWWRootName { get; set; }
        public DateTime UploadedAt { get; set; }
        public string extention { get; set; }
        public string Owner { get; set; }  
        public string Path { get; set; }
        public long Size { get; set; }


    }
}
