using Application.Dtos;
using DocumentManagementSystem.Services.ResultPattern;


namespace Application.IServices
{
    public interface IDocumentService
    {
        public Task <GenericResult> UploadeDocument(DocumentInputDto documentDto);
        public Task <GenericResult> GetDocument(string name);
        public Task <GenericResult> GetDocumentsByDirectoryName(string name, int page, int pageSize);
        public Task<GenericResult> AdminGetDocumentsByDirectoryName(string name, string userId, int page, int pageSize);

        public Task <GenericResult> GetDocuments(int page, int pageSize);
        public Task <GenericResult> GetSharedDocuments(int page, int pageSize);

        public Task <GenericResult> SortByNameAscending(string name, int page, int pageSize);
        public Task <GenericResult> SortBySizeAscending(string name, int page, int pageSize);
        public Task <GenericResult> SortByDateAscending(string name, int page, int pageSize);
        public Task<GenericResult> SortByNameDescending(string name, int page, int pageSize);
        public Task<GenericResult> SortBySizeDescending(string name, int page, int pageSize);
        public Task<GenericResult> SortByDateDescending(string name, int page, int pageSize);
        public Task <(bool Success, byte[] bytes, string contentType, string name, string Message)> DownloadDocument(string name, string wwwRootName);
        public Task <GenericResult> DeleteDocument(string name);

        public Task <GenericResult> EditDocument(string name, string NewName);


        public Task<GenericResult> GetSharedDocumentSortedByDateDescending(int page, int pageSize);
        public Task<GenericResult> GetSharedDocumentSortedByDateAscending(int page, int pageSize);
        public Task<GenericResult> GetSharedDocumentSortedByNameDescending(int page, int pageSize);
        public Task<GenericResult> GetSharedDocumentSortedByNameAscending(int page, int pageSize);
        public Task<GenericResult> GetSharedDocumentSortedBySizeDescending(int page, int pageSize);
        public Task<GenericResult> GetSharedDocumentSortedBySizeAscending(int page, int pageSize);

        public Task<GenericResult> SearchDocuments(string filter, int page, int pageSize);
        public Task<GenericResult> SearchSharedDocuments(string filter, int page, int pageSize);
        Task<GenericResult> AdminSearchDocumentsByDirectoryName(string filter, string name, string email, int page, int pageSize);








    }
}
