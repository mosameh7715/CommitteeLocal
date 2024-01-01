﻿using Committees.Application.Features.Proceedings.GetById;

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
				//.ForMember(dest => dest.InternalMembers, opt => opt.Ignore())
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
				//.ForMember(dest => dest.InternalMembers, opt => opt.Ignore())
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

            // OutputType
            CreateMap<PostOutputTypeCommand, OutputType>();
            CreateMap<PutOutputTypeCommand, OutputType>();
            CreateMap<OutputType, AllOutputTypesDTO>();

			#region CommitteeApprovals
			CreateMap<Committee,AllCommitteeApprovalDto>()
				.ForMember(des => des.CommitteeKeeperId,opt =>
				{
					opt.MapFrom(src =>
						(src.CommitteeInternalMembers
							.Where(i => i.InternalMember.Permission.NameAr.Contains("امين اللجنة") || i.InternalMember.Permission.NameEn.Contains("Committee Keeper"))
							.Select(i => i.InternalMemberId)
							.FirstOrDefault()) != default(Guid)
							? src.CommitteeInternalMembers
								.Where(i => i.InternalMember.Permission.NameAr.Contains("امين اللجنة") || i.InternalMember.Permission.NameEn.Contains("Committee Keeper"))
								.Select(i => i.InternalMemberId)
								.FirstOrDefault()
							: (src.ExternalMembers
								.Where(e => e.Permission.NameAr.Contains("امين اللجنة") || e.Permission.NameEn.Contains("Committee Keeper"))
								.Select(e => e.Id)
								.FirstOrDefault()) != default(Guid)
								? src.ExternalMembers
									.Where(e => e.Permission.NameAr.Contains("امين اللجنة") || e.Permission.NameEn.Contains("Committee Keeper"))
									.Select(e => e.Id)
									.FirstOrDefault()
								: default(Guid)
					);
				})
				.ForMember(des => des.CommitteeKeeperName,opt =>
				{
					opt.MapFrom(src =>
						src.ExternalMembers
							.Where(e => e.Permission.NameAr.Contains("امين اللجنة") || e.Permission.NameEn.Contains("Committee Keeper"))
							.Select(e => e.Name)
							.FirstOrDefault() ?? string.Empty
					);
				})
				.ForMember(des => des.MemberCount,opt => opt.MapFrom(src => src.CommitteeInternalMembers.Count() + src.ExternalMembers.Count()));

			CreateMap<AllCommitteeApprovalDto,AllCommitteeApprovalProto>()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Name.ToString()))
				.ForMember(dest => dest.Description,opt => opt.MapFrom(src => src.Description ?? ""))
				.ForMember(dest => dest.Missions,opt => opt.MapFrom(src => src.Missions ?? ""))
				.ForMember(dest => dest.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
				.ForMember(dest => dest.CommitteeTime,opt => opt.MapFrom(src => src.CommitteeTime))
				.ForMember(dest => dest.CommitteeKeeperId,opt => opt.MapFrom(src => src.CommitteeKeeperId.ToString()))
				.ForMember(dest => dest.CommitteeKeeperName,opt => opt.MapFrom(src => src.CommitteeKeeperName ?? ""))
				.ForMember(dest => dest.CommitteesStatus,opt => opt.MapFrom(src => src.CommitteesStatus))
				.ForMember(dest => dest.MemberCount,opt => opt.MapFrom(src => src.MemberCount));

			CreateMap<Committee,CommitteeApprovalByIdDto>()
				.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMembers.Select(x => x.Id)))
				.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.CommitteeInternalMembers.Select(x => x.InternalMemberId)))
				.ForMember(des => des.Attachments,opt => opt.MapFrom(src => src.Attachments.Select(x => x.Path)));

			CreateMap<CommitteeApprovalByIdDto,CommitteeApprovalProtoById>()
				.ForMember(des => des.Name,opt => opt.MapFrom(src => src.Name ?? ""))
				.ForMember(des => des.Description,opt => opt.MapFrom(src => src.Description ?? ""))
				.ForMember(des => des.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(des => des.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
				.ForMember(des => des.Missions,opt => opt.MapFrom(src => src.Missions ?? ""))
				.ForMember(des => des.WorkRule,opt => opt.MapFrom(src => src.WorkRule ?? ""))
				.ForMember(des => des.HasLegalDocument,opt => opt.MapFrom(src => src.HasLegalDocument))
				.ForMember(des => des.LegalDocument,opt => opt.MapFrom(src => src.LegalDocument ?? ""))
				.ForMember(des => des.CommitteeTime,opt => opt.MapFrom(src => (int?)src.CommitteeTime))
				.ForMember(des => des.CommitteesStatus,opt => opt.MapFrom(src => (int?)src.CommitteesStatus))
				.ForMember(des => des.Attachments,opt => opt.MapFrom(src => src.Attachments.Select(x => x ?? "")))
				.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMembers.Select(x => x.ToString() ?? "")))
				.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.InternalMembers.Select(x => x.ToString() ?? "")));
			#endregion

			#region Committee
			CreateMap<Committee,GetAllCommitteesDto>()
					.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMembers.Select(x => x.Id)))
					.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.CommitteeInternalMembers.Select(x => x.InternalMemberId)));

			CreateMap<GetAllCommitteesDto,AllCommitteeProto>()
					.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
					.ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Name.ToString()))
					.ForMember(dest => dest.Description,opt => opt.MapFrom(src => src.Description ?? ""))
					.ForMember(dest => dest.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
					.ForMember(dest => dest.CommitteesStatus,opt => opt.MapFrom(src => src.CommitteesStatus))
					.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMembers.Select(x => x.ToString() ?? "")))
					.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.InternalMembers.Select(x => x.ToString() ?? "")));
			#endregion

			#region MeetingAttachments
			CreateMap<MeetingAttachment,MeetingAttachmentDto>();

			CreateMap<MeetingAttachmentDto,AllMeetingAttachmentProto>()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Path,opt => opt.MapFrom(src => src.Path.ToString() ?? ""))
				.ForMember(dest => dest.CreatedBy,opt => opt.MapFrom(src => src.CreatedBy.ToString()))
				.ForMember(dest => dest.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""));
			#endregion

			#region OutputAttachments
			CreateMap<OutputAttachment,OutputAttachmentDto>();

			CreateMap<OutputAttachmentDto,AllOutputAttachmentProto>()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Path,opt => opt.MapFrom(src => src.Path.ToString() ?? ""))
				.ForMember(dest => dest.CreatedBy,opt => opt.MapFrom(src => src.CreatedBy.ToString()))
				.ForMember(dest => dest.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""));
			#endregion

			#region ExternalMembers
			CreateMap<ExternalMember,AllExternalMembersDto>()
				.ForMember(des => des.PermissionNameAr,opt => opt.MapFrom(src => src.Permission.NameAr))
				.ForMember(des => des.PermissionNameEn,opt => opt.MapFrom(src => src.Permission.NameEn));

			CreateMap<AllExternalMembersDto,ExternalMemberProto>()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Name ?? ""))
				.ForMember(dest => dest.DestinationName,opt => opt.MapFrom(src => src.DestinationName ?? ""))
				.ForMember(dest => dest.Email,opt => opt.MapFrom(src => src.Email ?? ""))
				.ForMember(dest => dest.Job,opt => opt.MapFrom(src => src.Job ?? ""))
				.ForMember(dest => dest.PermissionNameAr,opt => opt.MapFrom(src => src.PermissionNameAr ?? ""))
				.ForMember(dest => dest.PermissionNameEn,opt => opt.MapFrom(src => src.PermissionNameEn ?? ""))
				.ForMember(dest => dest.PhoneNumber,opt => opt.MapFrom(src => src.PhoneNumber ?? ""));
			#endregion

			#region InternalMembers
			CreateMap<InternalMember,AllInternalMembersDto>()
				.ForMember(des => des.PermissionNameAr,opt => opt.MapFrom(src => src.Permission.NameAr))
				.ForMember(des => des.PermissionNameEn,opt => opt.MapFrom(src => src.Permission.NameEn));

			CreateMap<AllInternalMembersDto,InternalMemberProto>()
				.ForMember(dest => dest.UserId,opt => opt.MapFrom(src => src.UserId.ToString()))
				.ForMember(dest => dest.PermissionNameAr,opt => opt.MapFrom(src => src.PermissionNameAr ?? ""))
				.ForMember(dest => dest.PermissionNameEn,opt => opt.MapFrom(src => src.PermissionNameEn ?? ""));
			#endregion

			#region ProceedingExternalMembers
			CreateMap<ExternalMember,AllProceedingExternalMembersDto>()
				.ForMember(des => des.PermissionNameAr,opt => opt.MapFrom(src => src.Permission.NameAr))
				.ForMember(des => des.PermissionNameEn,opt => opt.MapFrom(src => src.Permission.NameEn));

			CreateMap<AllProceedingExternalMembersDto,ProceedingExternalMemberProto> ()
				.ForMember(dest => dest.Id,opt => opt.MapFrom(src => src.Id.ToString()))
				.ForMember(dest => dest.Name,opt => opt.MapFrom(src => src.Name ?? ""))
				.ForMember(dest => dest.DestinationName,opt => opt.MapFrom(src => src.DestinationName ?? ""))
				.ForMember(dest => dest.Email,opt => opt.MapFrom(src => src.Email ?? ""))
				.ForMember(dest => dest.Job,opt => opt.MapFrom(src => src.Job ?? ""))
				.ForMember(dest => dest.PermissionNameAr,opt => opt.MapFrom(src => src.PermissionNameAr ?? ""))
				.ForMember(dest => dest.PermissionNameEn,opt => opt.MapFrom(src => src.PermissionNameEn ?? ""))
				.ForMember(dest => dest.PhoneNumber,opt => opt.MapFrom(src => src.PhoneNumber ?? ""))
				.ForMember(dest => dest.IsAttend,opt => opt.MapFrom(src => src.IsAttend));
			#endregion

			#region ProceedingInternalMembers
			CreateMap<InternalMember,AllProceedingInternalMembersDto>()
				.ForMember(des => des.PermissionNameAr,opt => opt.MapFrom(src => src.Permission.NameAr))
				.ForMember(des => des.PermissionNameEn,opt => opt.MapFrom(src => src.Permission.NameEn));

			CreateMap<AllProceedingInternalMembersDto,ProceedingInternalMemberProto>()
				.ForMember(dest => dest.UserId,opt => opt.MapFrom(src => src.UserId.ToString()))
				.ForMember(dest => dest.PermissionNameAr,opt => opt.MapFrom(src => src.PermissionNameAr ?? ""))
				.ForMember(dest => dest.IsAttend,opt => opt.MapFrom(src => src.IsAttend))
				.ForMember(dest => dest.PermissionNameEn,opt => opt.MapFrom(src => src.PermissionNameEn ?? ""));
			#endregion

			#region Proceeding
			CreateMap<Proceeding,GetProceedingByIdDto>()
					.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMemberProceedings.Select(x => x.ExternalMemberId)))
					.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.InternalMemberProceedings.Select(x => x.InternalMemberId)))
					.ForMember(des => des.Attachments,opt => opt.MapFrom(src => src.ProceedingAttachments.Select(x => x.Path)));

			CreateMap<GetProceedingByIdDto,ProceedingByIdProto>()
					.ForMember(des => des.Name,opt => opt.MapFrom(src => src.Name ?? ""))
					.ForMember(des => des.Notes,opt => opt.MapFrom(src => src.Notes ?? ""))
					.ForMember(des => des.Id,opt => opt.MapFrom(src => src.Id.ToString()))
					.ForMember(des => des.CreatedOn,opt => opt.MapFrom(src => src.CreatedOn.ToString() ?? ""))
					.ForMember(des => des.Attachments,opt => opt.MapFrom(src => src.Attachments.Select(x => x ?? "")))
					.ForMember(des => des.ExternalMembers,opt => opt.MapFrom(src => src.ExternalMembers.Select(x => x.ToString() ?? "")))
					.ForMember(des => des.InternalMembers,opt => opt.MapFrom(src => src.InternalMembers.Select(x => x.ToString() ?? "")));
			#endregion
		}
	}
}

