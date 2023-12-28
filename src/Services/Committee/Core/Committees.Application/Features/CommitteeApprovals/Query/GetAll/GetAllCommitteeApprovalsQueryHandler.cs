namespace Committees.Application.Features.CommitteeApprovals.Query.GetAll
{
	public class GetAllCommitteeApprovalsQueryHandler:IRequestHandler<GetAllCommitteeApprovalsQuery,ResponseDTO>
	{
		private readonly IGRepository<Committee> _committeeRepo;
		private readonly IMapper _mapper;
		private readonly IResponseHelper _responseHelper;
		private ResponseDTO _responseDTO;

		public GetAllCommitteeApprovalsQueryHandler(IMapper mapper,
								IResponseHelper responseHelper,
								IGRepository<Committee> committeeRepo)
		{
			_mapper = mapper;
			_responseHelper = responseHelper;
			_committeeRepo = committeeRepo;
			_responseDTO = new ResponseDTO();
		}
		public async Task<ResponseDTO> Handle(GetAllCommitteeApprovalsQuery request,CancellationToken cancellationToken)
		{
			var allCommittes = _committeeRepo.GetAll(request.PageIndex,request.PageSize,ref _responseDTO).OrderBy(x => x.CreatedOn).ToList();

			string searchTerm = request.SearchTerm ?? string.Empty;

			if(allCommittes.Any())
			{
				var filteredCommittees = allCommittes.Where(c =>
					c.Name.Contains(searchTerm,StringComparison.OrdinalIgnoreCase) ||
					c.CommitteesStatus == (CommitteesStatus)(Int32.TryParse(searchTerm,out int parsedStatus) ? parsedStatus : 0)
				).ToList();

				var committeeMapped = _mapper.Map<List<AllCommitteeApprovalDto>>(filteredCommittees);

				return _responseHelper.RetrievedSuccessfully(committeeMapped,"CommitteeApprovalsRetrievedSuccessfully!",ref _responseDTO);
			}
			return _responseHelper.RetrievedSuccessfully(allCommittes,"CommitteeApprovalsRetrievedSuccessfully!",ref _responseDTO);
		}
	}
}
