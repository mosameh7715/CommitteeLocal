namespace Committees.Application.Features.Committees.Queries.GetAll
{
	public class GetAllCommitteesQueryHandler:IRequestHandler<GetAllCommitteesQuery,ResponseDTO>
	{
		private readonly IGRepository<Committee> _committeeRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;
		private ResponseDTO _responseDTO;

		public GetAllCommitteesQueryHandler(IResponseHelper responseHelper,
										  IGRepository<Committee> committeeRepo,
										  IMapper mapper,
										  IHttpContextAccessor _httpContextAccessor)
		{
			_committeeRepo = committeeRepo;
			_responseHelper = responseHelper;
			_responseDTO = new ResponseDTO();
			_mapper = mapper;
		}
		public async Task<ResponseDTO> Handle(GetAllCommitteesQuery request,CancellationToken cancellationToken)
		{
			var committeee = _committeeRepo.GetAll(request.PageIndex,request.PageSize,ref _responseDTO)
											  .Include(x => x.ExternalMembers.Where(c => c.State == State.NotDeleted))
											  .Include(x => x.CommitteeInternalMembers.Where(a => a.State == State.NotDeleted))
											  .ThenInclude(x => x.InternalMember)
											  .OrderByDescending(x => x.CreatedOn)
											  .ToList();

			string searchTerm = request.SearchTerm ?? string.Empty;

			if (committeee.Any())
			{
				var filteredCommittees = committeee.Where(c =>
					c.Name.Contains(searchTerm,StringComparison.OrdinalIgnoreCase) ||
					c.CommitteesStatus == (CommitteesStatus)(Int32.TryParse(searchTerm,out int parsedStatus) ? parsedStatus : 0))
					.ToList();

				var committeeMapped = _mapper.Map<List<GetAllCommitteesDto>>(filteredCommittees);

				return _responseHelper.RetrievedSuccessfully(committeeMapped,"CommitteesRetrievedSuccessfully!",ref _responseDTO);
			}

			return _responseHelper.RetrievedSuccessfully(committeee,"CommitteesRetrievedSuccessfully!",ref _responseDTO);
		}
	}
}
