using Application.Dtos;
using AutoMapper;
using DocumentManagementSystem.Services.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MapperHelper
{
    public class ProfileMapper : Profile
    {
        public ProfileMapper()
        {
            CreateMap<AppUser, UserDto>();
            CreateMap<AppUser, UserOutputDto>()
                .ForMember(dest => dest.WorkSpaceName, opt => opt.MapFrom(src => src.WorkSpace.Name))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));


            CreateMap<Domain.Models.Directory, DirectoryDto>()
                 .ForMember(dest => dest.WorkspaceName, opt => opt.MapFrom(src => src.WorkSpace.Name))
                 .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.AppUser.UserName));

            CreateMap<Document, DocumentOutputDto>();

                

        }
    }
}
