using AutoMapper;
using Domain.DTOs;
using Domain.Entities;
using N5_challenge.Commands;

namespace N5_challenge.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, GetPermissionDto>()
              .ForMember(dest => dest.PermissionType, opt => opt.MapFrom(src => src.PermissionType.Description))
              .ForMember(dest => dest.PermissionId, opt => opt.MapFrom(src => src.Id));

            CreateMap<CreatePermissionCommand, Permission>();
            CreateMap<UpdatePermissionCommand, Permission>();
                //.ForPath(dst => dst.permissionTypeId, opt => opt.MapFrom(src => src.permissionTypeId));
        }
    }
}
