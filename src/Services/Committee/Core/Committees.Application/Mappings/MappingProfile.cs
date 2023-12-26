namespace Committees.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Permission
            CreateMap<PostPermissionCommand, Permission>();
            CreateMap<PutPermissionCommand, Permission>();
            CreateMap<Permission, AllPermissionsDTO>();

            // OutputType
            CreateMap<PostOutputTypeCommand, OutputType>();
            CreateMap<PutOutputTypeCommand, OutputType>();
            CreateMap<OutputType, AllOutputTypesDTO>();
        }
	}
}