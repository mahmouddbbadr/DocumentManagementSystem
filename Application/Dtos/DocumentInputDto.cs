using Microsoft.AspNetCore.Http;



namespace Application.Dtos
{
    public class DocumentInputDto
    {
        public IFormFile File { get; set; }
        public List<string>? Tags { get; set; }
        public string DirectoryName { get; set; }

    }
}
