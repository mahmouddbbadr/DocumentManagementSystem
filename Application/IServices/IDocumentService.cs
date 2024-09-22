using Application.Dtos;
using DocumentManagementSystem.Services.ResultPattern;


namespace Application.IServices
{
    public interface IDocumentService
    {
        public Task <GenericResult> UploadeDocument(DocumentInputDto documentDto);
        public Task <GenericResult> GetDocument(string name);
        public Task <GenericResult> GetDocumentsByDirectoryName(string name);
        public Task<GenericResult> AdminGetDocumentsByDirectoryName(string name, string userId);

        public Task <GenericResult> GetDocuments();
        public Task <GenericResult> GetSharedDocuments();

        public Task <GenericResult> SortByName();
        public Task <GenericResult> SortBySize();
        public Task <GenericResult> SortByDate();
        public Task <(bool Success, byte[] bytes, string contentType, string name, string Message)> DownloadDocument(string name, string wwwRootName);
        public Task <GenericResult> DeleteDocument(string name);

        public Task <GenericResult> EditDocument(string name, string NewName);




    }
}
