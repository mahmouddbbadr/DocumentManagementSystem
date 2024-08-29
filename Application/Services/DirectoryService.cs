using Application.Dtos;
using Application.IRepository;
using Application.IServices;
using AutoMapper;
using Domain.Models;
using Infrasturcture.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<(bool Success, string Message)> CreateDirectory(string name)
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
                    return (true, "Directory created successfully");
                    }
                    return (false, "Something went worng while creating directory, try again!");

                };
                return (false, $"Directory name \"{name}\" already exists");
                
            }
            return (false, "User was not found");
        }

        public async Task<(bool Success, DirectoryDto directory, string Message)> GetDirectory(string name)
        {
            var UsrId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var checkDirExsits = directoryRepository.CheckEntityExits(name, UsrId);
                if (checkDirExsits)
                {
                    var directory = mapper.Map<DirectoryDto>(directoryRepository.GetByIdOrName(name, UsrId));
                    return (true, directory, "");
                }
                return (false, null, $"No directory was found with name \"{name}\"");
            }
            return (false, null, "User was not found");
        }

        public async Task<(bool Success, ICollection<DirectoryDto> directories, string Message)> GetDirectoryies()
        {
            var UsrId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var directories = mapper.Map<ICollection<DirectoryDto>>(directoryRepository.GetAll(UsrId));
                if (directories != null)
                {
                    return(true, directories, "");
                }
                return(false, null, "No directories was found for this user");
            }
            return(false, null, "User was not found");
        }

        public async Task<(bool Success, string Message)> DeleteDirectory(string name)
        {

            var UsrId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var exists = directoryRepository.CheckEntityExits(name, UsrId);
                if (exists)
                {
                    var directory = directoryRepository.Delete(name, UsrId);
                    return(true, $"Directory \"{name}\" was deleted successfully");
                }
                return(false, $"Can not find Directory name \"{name}\"");
            }
            return(false, "User was not found");
        }

        public async Task<(bool Success, string Message)> MakeDirectoryPublic(string name)
        {
            var UsrId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var exists = directoryRepository.CheckEntityExits(name, UsrId);
                if (exists)
                {
                    var directory = directoryRepository.GetByIdOrName(name, UsrId);
                    directory.IsPrivate = false;
                    directoryRepository.Save();
                    return(true, $"Directory \"{name}\" has been made public successfully");
                }
                return(false, $"Can not find Directory name \"{name}\"");
            }
            return(false, "User was not found");
        }

        public async Task<(bool Success, string Message)> MakeDirectoryPrivate(string name)
        {
            var UsrId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (UsrId != null)
            {
                var exists = directoryRepository.CheckEntityExits(name, UsrId);
                if (exists)
                {
                    var directory = directoryRepository.GetByIdOrName(name, UsrId);
                    directory.IsPrivate = true;
                    directoryRepository.Save();
                    return(true, $"Directory \"{name}\" has been made private successfully");
                }
                return(false, $"Can not find Directory name \"{name}\"");
            }
            return(false, "User was not found");
        }
    }
}
