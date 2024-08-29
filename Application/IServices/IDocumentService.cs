using Application.Dtos;
using DocumentManagementSystem.Services.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IDocumentService
    {
        public Task<(bool Success, string Message)> UploadeDocument(DocumentInputDto documentDto);
        public Task<(bool Success, string Path, string Message)> DownloadDocument(Guid id);
        public Task<(bool Success, string Message)> DeleteDocument(string name);
        public Task<(bool Success,DocumentOutputDto document, string Message)> GetDocument(string name);
        public Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> GetDocuments();
        public Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortByName();
        public Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortBySize();
        public Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortByDate();



    }
}
