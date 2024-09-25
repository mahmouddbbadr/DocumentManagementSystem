using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using DocumentManagementSystem.Domain.IRepository;
using DocumentManagementSystem.Services.Dtos;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.StaticFiles;
using DocumentManagementSystem.Services.ResultPattern;
using System.Drawing.Printing;


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


        public async Task<GenericResult> GetDocument(string name)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var document = documentRepository.GetByIdOrName(name, userId);
                if (document != null)
                {
                    var documentDto = mapper.Map<DocumentOutputDto>(document);
                    return (new GenericResult() { Success = true, Body = documentDto, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "Document was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> GetDocuments(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAll(userId);
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null});
                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }

        public async Task<GenericResult> GetSharedDocuments(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllShared();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);            
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null});

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> GetSharedDocumentSortedByNameAscending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedByNameAscending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> GetSharedDocumentSortedByNameDescending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedByNameDescending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> GetSharedDocumentSortedBySizeAscending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedBySizeAscending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> GetSharedDocumentSortedBySizeDescending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedBySizeDescending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> GetSharedDocumentSortedByDateAscending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedByDeteAscending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> GetSharedDocumentSortedByDateDescending(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.GetAllSharedSortedByDateDescending();
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }


        public async Task<GenericResult> UploadeDocument(DocumentInputDto documentDto)
        {

            List<string> validExtentions = new List<string>() { ".docx", ".pdf", ".txt", ".xlsx", ".jpg", ".jpeg", ".png" };
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                var directory = directoryRepository.GetByIdOrName(documentDto.DirectoryName, userId);
                if (directory != null)
                {
                    var documentExists = documentRepository.CheckDocumentExits(documentDto.File.FileName, userId, documentDto.DirectoryName);
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
                                WWWRootName = fileName,
                                Path = path,
                                Size = documentDto.File.Length,
                                UploadedAt = DateTime.Now,
                                extention = extention,
                                Owner = user.Email,
                                DirectoryId = directory.Id,
                                UserId = userId,
                                Tags = new List<Tag>()
                            };
                            documentRepository.Create(document);
                            foreach (var doctag in documentDto?.Tags)
                            {
                                var tagExists = tagRepository.Get(doctag);
                                if (tagExists == null)
                                {
                                    var tag = new Tag() { Name = doctag };
                                    tagRepository.Create(tag);
                                    document.Tags.Add(tag);
                                }
                                document.Tags.Add(tagExists);
                            }
                            return (new GenericResult() { Success = true, Body = null, Message = "Document added successfully" });

                        }
                        return (new GenericResult() { Success = false, Body = null, Message = $"Extention is not valid, valid extentions are ({string.Join(',', validExtentions)})" });


                    }
                    return (new GenericResult() { Success = false, Body = null, Message = $"Document name \"{documentDto.File.FileName}\" already exists" });

                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was found with name \"{documentDto.DirectoryName}\"" });


            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SortByNameAscending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                { 
                    var documents = documentRepository.SortByNameAscending(directory.Id, userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });
                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SortBySizeAscending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                { 
                    var documents = documentRepository.SortBySizeAscending(directory.Id, userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });
                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }


        public async Task<GenericResult> SortByDateAscending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                { 
                    var documents = documentRepository.SortByDateAscending(directory.Id, userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SortByNameDescending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                {
                    var documents = documentRepository.SortByNameDescending(directory.Id, userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });
                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SortBySizeDescending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                { 
                    var documents = documentRepository.SortBySizeDescending(directory.Id,userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });
                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }


        public async Task<GenericResult> SortByDateDescending(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if (directory != null)
                {
                    var documents = documentRepository.SortByDateDescending(directory.Id,userId);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });
            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> DeleteDocument(string name)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var document = documentRepository.GetByIdOrName(name, userId);
                if (document != null)
                {
                    if (document.UserId == userId)
                    {
                        documentRepository.Delete(name, userId);
                        return (new GenericResult() { Success = true, Body = null, Message = "Document deleted successfully" });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "You can not delete this, it is not yours!" });

                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Document name \"{name}\" was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<(bool Success, byte[] bytes, string contentType, string name, string Message)> DownloadDocument(string name, string wwwRootName)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var document = documentRepository.GetByIdOrName(name, userId);
                if (document != null)
                {
                    var path = Path.Combine(environment.WebRootPath, "SavedDocuments", wwwRootName);
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(path, out var contentType))
                    {
                        contentType = "application/octet-stream";
                    }
                    var bytes = await File.ReadAllBytesAsync(path);
                    return (true, bytes, contentType, document.Name, "");

                }
                return (false, null, null, null, $"Document was not found");
            }
            return (false, null, null, null, "User was not found");
        }

        public async Task<GenericResult> GetDocumentsByDirectoryName(string name, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, userId);
                if(directory != null)
                {
                    var documents = documentRepository.GetByDirectoryId(directory.Id);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    if (documents.Count != 0)
                    {
                        var Mappeddocuments = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = Mappeddocuments, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was not found with name \"{name}\"" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> AdminGetDocumentsByDirectoryName(string name, string email, int page, int pageSize)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, user.Id);
                if(directory != null)
                {
                    var documents = documentRepository.GetByDirectoryId(directory.Id);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    if (documents.Count != 0)
                    {
                        var Mappeddocuments = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = Mappeddocuments, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Directory \"{name}\" was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
        public async Task<GenericResult> EditDocument(string OldName, string NewName)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var DocExists = documentRepository.CheckEntityExits(OldName, userId);
                if (DocExists)
                {
                    if (!documentRepository.CheckEntityExits(NewName, userId) | NewName == OldName)
                    {
                        var document = documentRepository.GetByIdOrName(OldName, userId);
                        document.Name = NewName;
                        directoryRepository.Save();
                        var mappedDocument = mapper.Map<DocumentOutputDto>(document);
                        return (new GenericResult() { Success = true, Body = mappedDocument, Message = "changes saved successfully" });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = $"Directory name \"{NewName}\" already Exists" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Can not find Document name \"{OldName}\"" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SearchDocuments(string filter, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.Search(filter, userId);
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });
                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }

        public async Task<GenericResult> SearchSharedDocuments(string filter, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var documents = documentRepository.SearchAllShared(filter);
                var totalCount = documents.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (documents.Count != 0)
                {
                    var documentDtos = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                    return (new GenericResult() { Success = true, Body = new { Documents = documentDtos, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }


        public async Task<GenericResult> AdminSearchDocumentsByDirectoryName(string filter, string name, string email, int page, int pageSize)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var directory = directoryRepository.GetByIdOrName(name, user.Id);
                if (directory != null)
                {
                    var documents = documentRepository.AdminSearch(filter, directory.Id);
                    var totalCount = documents.Count;
                    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                    documents = documents.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                    if (documents.Count != 0)
                    {
                        var Mappeddocuments = mapper.Map<ICollection<DocumentOutputDto>>(documents);
                        return (new GenericResult() { Success = true, Body = new { Documents = Mappeddocuments, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "No documents was not found" });
                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Directory \"{name}\" was not found" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
    }
}
