using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using DocumentManagementSystem.Services.Dtos;
using DocumentManagementSystem.Services.ResultPattern;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;


namespace Infrasturcture.Services
{
    public class DirectoryService : IDirectoryService
    {
        private readonly IGenericRepository<Domain.Models.Directory> directoryRepository;
        private readonly UserManager<AppUser> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public DirectoryService(IGenericRepository<Domain.Models.Directory> directoryRepository, 
            UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.directoryRepository = directoryRepository;
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }

        public async Task<GenericResult> CreateDirectory(string name)
        {

            var userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                var exists = directoryRepository.CheckEntityExits(name, userId);
                if (!exists)
                {
                    var directory = new Domain.Models.Directory()
                    {
                        Name = name,
                        UserId = userId,
                        WorkSpaceId = user.WorkspaceId
                    };
                    var createdDirectory = directoryRepository.Create(directory);
                    if (createdDirectory)
                    {
                    return (new GenericResult() { Success = true, Body = null, Message = "Directory created successfully" });
                    }
                    return (new GenericResult() { Success = false, Body = null, Message = "Something went worng while creating directory, try again!" });


                };
                return (new GenericResult() { Success = false, Body = null, Message = $"Directory name \"{name}\" already exists" });


            }
            return (new GenericResult(){Success = false, Body = null, Message = "User was not found"});

        }

        public async Task<GenericResult> GetDirectory(string name)
        {
            var UsrId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var checkDirExsits = directoryRepository.CheckEntityExits(name, UsrId);
                if (checkDirExsits)
                {
                    var directory = mapper.Map<DirectoryDto>(directoryRepository.GetByIdOrName(name, UsrId));
                    return (new GenericResult() { Success = true, Body = directory, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = $"No directory was found with name \"{name}\"" });


            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> GetDirectoryies(int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directories = mapper.Map<ICollection<DirectoryDto>>(directoryRepository.GetAll(userId));
                var totalCount = directories.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                directories = directories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (directories != null)
                {
                    return (new GenericResult() { Success = true, Body = new { Directories = directories, TotalCount = totalCount, TotalPages = totalPages }, Message = null});

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No directories was found for this user" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }
       
        public async Task<GenericResult> AdminGetDirectoryies(string email, int page, int pageSize)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var directories = mapper.Map<ICollection<DirectoryDto>>(directoryRepository.GetAll(user.Id));
                var totalCount = directories.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                directories = directories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (directories != null)
                {
                    return (new GenericResult() { Success = true, Body = new { Directories = directories, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No directories was found for this user" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }

        public async Task<GenericResult> DeleteDirectory(string name)
        {

            var UsrId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var exists = directoryRepository.CheckEntityExits(name, UsrId);
                if (exists)
                {
                    var directory = directoryRepository.Delete(name, UsrId);
                    return (new GenericResult() { Success = true, Body = null, Message = $"Directory \"{name}\" was deleted successfully" });

                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Can not find Directory name \"{name}\"" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }


        public async Task<GenericResult> EditDirectory(string name, DirectoryOutputDto directoryDto)
        {
            var UsrId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var exists = directoryRepository.CheckEntityExits(name, UsrId);
                if (exists)
                {
                    if (!directoryRepository.CheckEntityExits(directoryDto.Name, UsrId) | directoryDto.Name == name)
                    {
                        var directory = directoryRepository.GetByIdOrName(name, UsrId);
                        directory.IsPrivate = directoryDto.IsPrivate;
                        directory.Name = directoryDto.Name;
                        directoryRepository.Save();
                        var mappedDirectory = mapper.Map<DirectoryOutputDto>(directory);
                        return (new GenericResult() { Success = true, Body = mappedDirectory, Message = "changes saved successfully" });

                    }
                    return (new GenericResult() { Success = false, Body = null, Message = $"Directory name \"{directoryDto.Name}\" already Exists" });


                }
                return (new GenericResult() { Success = false, Body = null, Message = $"Can not find Directory name \"{name}\"" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });

        }

        public async Task<GenericResult> SearchDirectoryies(string filter, int page, int pageSize)
        {
            var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                var directories = mapper.Map<ICollection<DirectoryDto>>(directoryRepository.Search(filter, userId));
                var totalCount = directories.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                directories = directories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                if (directories.Count !=0)
                {
                    return (new GenericResult() { Success = true, Body = new { Directories = directories, TotalCount = totalCount, TotalPages = totalPages }, Message = null });

                }
                return (new GenericResult() { Success = false, Body = null, Message = "No directories was found for this user" });

            }
            return (new GenericResult() { Success = false, Body = null, Message = "User was not found" });
        }


    }


}
