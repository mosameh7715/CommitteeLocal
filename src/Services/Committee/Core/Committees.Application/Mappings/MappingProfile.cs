namespace Committees.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
			#region CommitteeApprovals
			CreateMap<Committee, AllCommitteeApprovalDto>();

			CreateMap<AllCommitteeApprovalDto,CommitteeProto>()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Name.ToString()))
				.ForMember(dest => dest.Missions,opt => opt.MapFrom(src => src.Missions ?? ""))
				.ForMember(dest => dest.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
				.ForMember(dest => dest.CommitteeTime,opt => opt.MapFrom(src => src.CommitteeTime))
				.ForMember(dest => dest.CommitteesStatus,opt => opt.MapFrom(src => src.CommitteesStatus));
			#endregion
		}
	}
}