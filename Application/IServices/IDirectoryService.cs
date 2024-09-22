using DocumentManagementSystem.Services.Dtos;
using DocumentManagementSystem.Services.ResultPattern;


namespace Application.IServices
{
    public interface IDirectoryService
    {
        public Task<GenericResult> GetDirectory(string name);
        public Task<GenericResult> GetDirectoryies();
        public Task<GenericResult> AdminGetDirectoryies(string userId);
        public Task<GenericResult> CreateDirectory(string name);
        public Task<GenericResult> DeleteDirectory(string name);
        public Task<GenericResult> EditDirectory(string name, DirectoryOutputDto directoryDto);

    }

}
