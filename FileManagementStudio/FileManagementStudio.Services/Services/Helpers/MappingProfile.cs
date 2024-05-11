using AutoMapper;
using FileManagementStudio.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.Services.Services.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<IFormFile, FileEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.Length))
                .ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.ContentType));
        }
    }
}
