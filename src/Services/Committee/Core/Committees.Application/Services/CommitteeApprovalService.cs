namespace Committees.Application.Services
{
	//[Authorize("ClientCredentialsPolicy")]
	public class CommitteeApprovalService :CommitteeApprovalProtoService.CommitteeApprovalProtoServiceBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public CommitteeApprovalService(IMediator mediator,
						  IMapper mapper,
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_mediator = mediator;
			_mapper = mapper;
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<ResponseAllCommitteeApprovals> GetAllCommitteeApprovals(RequestAllCommitteeApprovals request,ServerCallContext context)
		{
			var allCommittees = await _mediator.Send(new GetAllCommitteeApprovalsQuery
			{
				PageIndex = request.PageIndex,
				PageSize = request.PageSize,
				SearchTerm = request.SearchTerm
			});

			var responseMapped = _mapper.Map<List<CommitteeProto>>(allCommittees.Result);

			var response = new ResponseAllCommitteeApprovals();

			response.Committee.AddRange(responseMapped);
			response.PageIndex = allCommittees.PageIndex;
			response.TotalPages = allCommittees.TotalPages;
			response.TotalItems = allCommittees.TotalItems;
			response.PageSize = allCommittees.PageSize;

			return await Task.FromResult(response);
		}
	}
}
