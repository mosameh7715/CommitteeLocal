namespace Committees.Application.Features.CommitteeApprovals.Query.GetById
{
	public class GetCommitteeApprovalByIdQueryHandler:IRequestHandler<GetCommitteeApprovalByIdQuery,ResponseDTO>
	{
		private readonly IGRepository<Committee> _committeeRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetCommitteeApprovalByIdQueryHandler(IResponseHelper responseHelper,
										  IGRepository<Committee> committeeRepo,
										  IMapper mapper)
		{
			_committeeRepo = committeeRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
		}
		public async Task<ResponseDTO> Handle(GetCommitteeApprovalByIdQuery request,CancellationToken cancellationToken)
		{
			var committee = _committeeRepo.GetAll(x => x.Id == request.CommitteeId)
											  .Include(x => x.ExternalMembers.Where(c => c.State == State.NotDeleted))
											  .Include(x => x.CommitteeInternalMembers.Where(a => a.State == State.NotDeleted))
											  .ThenInclude(x => x.InternalMember)
											  .Include(x => x.Attachments.Where(a => a.State == State.NotDeleted))
											  .FirstOrDefault();

			if(committee == null)
			{
				return _responseHelper.NotFound("committeeIsNotExists");
			}

			var committeeMapped = _mapper.Map<CommitteeApprovalByIdDto>(committee);


			return _responseHelper.RetrievedSuccessfully(committeeMapped,"committeeIsRetrievedSuccessfully");
		}
	}
}
