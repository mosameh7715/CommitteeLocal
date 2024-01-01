namespace Committees.Application.Services
{
	//[Authorize("ClientCredentialsPolicy")]
	public class CommitteeService :CommitteeProtoService.CommitteeProtoServiceBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public CommitteeService(IMediator mediator,
						  IMapper mapper,
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_mediator = mediator;
			_mapper = mapper;
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<ResponseAllCommittees> GetAllCommittee(RequestAllCommittees request,ServerCallContext context)
		{
			var allCommittees = await _mediator.Send(new GetAllCommitteesQuery
			{
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				SearchTerm = request.SearchTerm
			});

			var responseMapped = _mapper.Map<List<AllCommitteeProto>>(allCommittees.Result);

			var response = new ResponseAllCommittees();

			response.Committee.AddRange(responseMapped);
			response.PageIndex = allCommittees.PageIndex;
			response.TotalPages = allCommittees.TotalPages;
			response.TotalItems = allCommittees.TotalItems;
			response.PageSize = allCommittees.PageSize;

			return await Task.FromResult(response);
		}

		public async override Task<ResponseAllCommitteeExternalMembers> GetAllCommitteeExternalMembers(RequestAllCommitteeExternalMembers request,ServerCallContext context)
		{
			var allExternalMembers = await _mediator.Send(new GetAllExternalMembersQuery
			{
				CommitteeId = Guid.TryParse(request.CommitteeId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<ExternalMemberProto>>(allExternalMembers.Result);

			var response = new ResponseAllCommitteeExternalMembers();

			response.ExternalMember.AddRange(responseMapped);

			return await Task.FromResult(response);
		}

		public async override Task<ResponseAllCommitteeInternalMembers> GetAllCommitteeInternalMembers(RequestAllCommitteeInternalMembers request,ServerCallContext context)
		{
			var allInternalMembers = await _mediator.Send(new GetAllInternalMembersQuery
			{
				CommitteeId = Guid.TryParse(request.CommitteeId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<InternalMemberProto>>(allInternalMembers.Result);

			var response = new ResponseAllCommitteeInternalMembers();

			response.InternalMember.AddRange(responseMapped);

			return await Task.FromResult(response);
		}
	}
}
