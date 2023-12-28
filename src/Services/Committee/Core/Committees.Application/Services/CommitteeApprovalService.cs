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

			var responseMapped = _mapper.Map<List<AllCommitteeApprovalProto>>(allCommittees.Result);

			var response = new ResponseAllCommitteeApprovals();

			response.Committee.AddRange(responseMapped);
			response.PageIndex = allCommittees.PageIndex;
			response.TotalPages = allCommittees.TotalPages;
			response.TotalItems = allCommittees.TotalItems;
			response.PageSize = allCommittees.PageSize;

			return await Task.FromResult(response);
		}

		public async override Task<ResponseCommitteeApprovalById> GetCommitteeApprovalById(RequestCommitteeApprovalById request,ServerCallContext context)
		{
			var committee = await _mediator.Send(new GetCommitteeApprovalByIdQuery { CommitteeId = Guid.TryParse(request.CommitteeId,out Guid parsedId) ? parsedId : default(Guid) });

			var responseMapped = _mapper.Map<CommitteeApprovalProtoById>(committee.Result);

			var response = new ResponseCommitteeApprovalById();

			response.Committee = responseMapped;

			return await Task.FromResult(response);
		}
	}
}
