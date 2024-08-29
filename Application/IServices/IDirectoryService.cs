using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.IServices
{
    public interface IDirectoryService
    {
        public Task<(bool Success, DirectoryDto directory, string Message)> GetDirectory(string name);
        public Task<(bool Success, ICollection<DirectoryDto> directories, string Message)> GetDirectoryies();
        public Task<(bool Success, string Message)> CreateDirectory(string name);
        public Task<(bool Success, string Message)> DeleteDirectory(string name);
        public Task<(bool Success, string Message)> MakeDirectoryPublic(string name);
        public Task<(bool Success, string Message)> MakeDirectoryPrivate(string name);

    }

}
