using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using Azure;
using DocumentManagementSystem.Domain.IRepository;
using DocumentManagementSystem.Services.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrasturcture.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository documentRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IWebHostEnvironment environment;
        private readonly IGenericRepository<Domain.Models.Directory> directoryRepository;
        private readonly ITagRepository tagRepository;

        public DocumentService(IDocumentRepository documentRepository, UserManager<AppUser> userManager,
            IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment environment,
            IGenericRepository<Domain.Models.Directory> directoryRepository, ITagRepository tagRepository)
        {
            this.documentRepository = documentRepository;
            this.userManager = userManager;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.environment = environment;
            this.directoryRepository = directoryRepository;
            this.tagRepository = tagRepository;
        }


        public async Task<(bool Success, DocumentOutputDto document, string Message)> GetDocument(string name)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var document = documentRepository.GetByIdOrName(name, userId);
                if (document != null)
                {
                    var documentDto = mapper.Map<DocumentOutputDto>(document);
                    documentDto.Content = await File.ReadAllBytesAsync(document.Path);
                    return (true, documentDto, "");
                }
                return (false, null, "Document was not found");
            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> GetDocuments()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAll(userId);
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    for (int i = 0; i < documents.Count; i++)
                    {
                        documentDtos.ElementAt(i).Content = await File.ReadAllBytesAsync(documents.ElementAt(i).Path);
                    }

                    return (true, documentDtos, "");
                }
                return (false, null, "No documents was not found");
            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, string Message)> UploadeDocument(DocumentInputDto documentDto)
        {

            List<string> validExtentions = new List<string>() { ".docx", ".pdf", ".xlsx", ".jpg", ".jpeg", ".png" };
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                var directory = directoryRepository.GetByIdOrName(documentDto.DirectoryName, userId);
                if (directory != null)
                {
                    var documentExists = documentRepository.CheckEntityExits(documentDto.File.FileName, userId);
                    if (!documentExists)
                    {
                        string extention = Path.GetExtension(documentDto.File.FileName).ToLower();
                        if (validExtentions.Contains(extention))
                        {
                            var fileName = $"{Guid.NewGuid()}_{documentDto.File.FileName}";
                            var path = Path.Combine(environment.WebRootPath, "SavedDocuments", fileName);
                            using FileStream stream = new FileStream(path, FileMode.Create);
                            documentDto.File.CopyTo(stream);
                            var document = new Domain.Models.Document()
                            {
                                Name = documentDto.File.FileName,
                                Path = path,
                                Size = documentDto.File.Length,
                                UploadedAt = DateTime.Now,
                                extention = extention,
                                Owner = user.UserName,
                                DirectoryId = directory.Id,
                                UserId = userId,
                                Tags = new List<Tag>()
                            };
                            documentRepository.Create(document);
                            foreach (var doctag in documentDto.Tags)
                            {
                                var tagExists = tagRepository.Get(doctag);
                                if (tagExists == null)
                                {
                                    var tag = new Tag() { Name = doctag};
                                    tagRepository.Create(tag);
                                    document.Tags.Add(tag);
                                }
                                document.Tags.Add(tagExists);
                            }
                            return (true, "Document added successfully");
                        }
                        return (false, $"Extention is not valid, valid extentions are ({string.Join(',', validExtentions)})");

                    }
                    return (false, $"Document name \"{documentDto.File.FileName}\" already exists");
                }
                return (false, $"No directory was found with name \"{documentDto.DirectoryName}\"");

            }
            return (false, "User was not found");
        }

        public async Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortByName()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.SortByName(userId);
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    for (int i = 0; i < documents.Count; i++)
                    {
                        documentDtos.ElementAt(i).Content = await File.ReadAllBytesAsync(documents.ElementAt(i).Path);
                    }

                    return (true, documentDtos, "");
                }
                return (false, null, "No documents was not found");
            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortBySize()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.SortBySize(userId);
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    for (int i = 0; i < documents.Count; i++)
                    {
                        documentDtos.ElementAt(i).Content = await File.ReadAllBytesAsync(documents.ElementAt(i).Path);
                    }

                    return (true, documentDtos, "");
                }
                return (false, null, "No documents was not found");
            }
            return (false, null, "User was not found");
        }
    

        public async Task<(bool Success, ICollection<DocumentOutputDto> documents, string Message)> SortByDate()
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.SortByDate(userId);
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    for (int i = 0; i < documents.Count; i++)
                    {
                        documentDtos.ElementAt(i).Content = await File.ReadAllBytesAsync(documents.ElementAt(i).Path);
                    }

                    return (true, documentDtos, "");
                }
                return (false, null, "No documents was not found");
            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, string Message)> DeleteDocument(string name)
         {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId != null)
            {
                var document = documentRepository.GetByIdOrName(name, userId);
                if(document != null)
                {
                    if(document.UserId == userId)
                    {
                        documentRepository.Delete(name, userId);
                        return (true, "Document deleted successfully");
                    }
                    return (false, "You can not delete this, it is not yours!");
                }
                return (false, $"Document name \"{name}\" was not found");
            }
            return (false, "User was not found");
        }

        public async Task<(bool Success, string Path, string Message)> DownloadDocument(Guid id)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var document = documentRepository.GetByIdOrName(id, userId);
                if (document != null)
                {
                    return (true, document.Path, "");
                }
                return (false, null, "Document was not found");
            }
            return (false, null, "User was not found");
        }

    }
    
}
