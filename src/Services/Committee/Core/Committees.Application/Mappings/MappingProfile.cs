using Committees.Application.Features.CommitteeFeatures.Command.Put;

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

			#region PostCommitteeMapping..
			// Committee
			CreateMap<PostCommitteeDto, Committee>()
				.ForMember(dest => dest.Attachments, opt => opt.Ignore())
				.ForMember(dest => dest.WorkRules, opt => opt.Ignore())
				.ForMember(dest => dest.ExternalMembers, opt => opt.Ignore())
				.ForMember(dest => dest.Targets, opt => opt.Ignore())
				.ForMember(dest => dest.Meetings, opt => opt.Ignore())
				.ForMember(dest => dest.InternalMembers, opt => opt.Ignore())
				.ForMember(dest => dest.Proceedings, opt => opt.Ignore())
				.ForMember(dest => dest.Outputs, opt => opt.Ignore())
				.ForMember(dest => dest.CommitteesStatus, opt => opt.MapFrom(src => CommitteesStatus.Pending));

			// Committee - CommitteeAttachments
			CreateMap<PostCommitteeDto, CommitteeAttachment>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Attachments));

			// Committee - WorkRuleAttachments
			CreateMap<PostCommitteeDto, WorkRule>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.WorkRules));

			// Committee - ExternalMembers
			CreateMap<PostCommitteeDto, ExternalMemberDto>()
				.ForMember(dest => dest.CommitteeId, opt => opt.Ignore());

			// Committee - Targets
			CreateMap<PostCommitteeDto, TargetDto>()
				.ForMember(dest => dest.CommitteeId, opt => opt.Ignore());
			#endregion

			#region PutCommitteeMapping

			// Committee
			CreateMap<PutCommitteeDto, Committee>()
				.ForMember(dest => dest.Attachments, opt => opt.Ignore())
				.ForMember(dest => dest.WorkRules, opt => opt.Ignore())
				.ForMember(dest => dest.ExternalMembers, opt => opt.Ignore())
				.ForMember(dest => dest.Targets, opt => opt.Ignore())
				.ForMember(dest => dest.Meetings, opt => opt.Ignore())
				.ForMember(dest => dest.InternalMembers, opt => opt.Ignore())
				.ForMember(dest => dest.Proceedings, opt => opt.Ignore())
				.ForMember(dest => dest.Outputs, opt => opt.Ignore())
				.ForMember(dest => dest.CommitteesStatus, opt => opt.MapFrom(src => CommitteesStatus.Pending));

			// Committee - CommitteeAttachments
			CreateMap<PutCommitteeDto, CommitteeAttachment>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Attachments));

			// Committee - WorkRuleAttachments
			CreateMap<PutCommitteeDto, WorkRule>()
				.ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.WorkRules));

			// Committee - ExternalMembers
			CreateMap<PutCommitteeDto, ExternalMemberDto>()
				.ForMember(dest => dest.CommitteeId, opt => opt.Ignore());

			// Committee - Targets
			CreateMap<PutCommitteeDto, TargetDto>()
				.ForMember(dest => dest.CommitteeId, opt => opt.Ignore());

            #endregion

            #region CommitteeApprovals
            CreateMap<Committee, AllCommitteeApprovalDto>();

            CreateMap<AllCommitteeApprovalDto, CommitteeProto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.ToString()))
                .ForMember(dest => dest.Missions, opt => opt.MapFrom(src => src.Missions ?? ""))
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
                .ForMember(dest => dest.CommitteeTime, opt => opt.MapFrom(src => src.CommitteeTime))
                .ForMember(dest => dest.CommitteesStatus, opt => opt.MapFrom(src => src.CommitteesStatus));
            #endregion
        }


    }
}
