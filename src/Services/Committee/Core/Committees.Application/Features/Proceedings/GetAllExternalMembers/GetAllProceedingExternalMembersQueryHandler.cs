namespace Committees.Application.Features.Proceedings.GetAllExternalMembers
{
	public class GetAllProceedingExternalMembersQueryHandler:IRequestHandler<GetAllProceedingExternalMembersQuery,ResponseDTO>
	{
		private readonly IGRepository<ExternalMember> _exMembersRepo;
		private readonly IGRepository<Proceeding> _proceedingRepo;
		private readonly IGRepository<ExternalMemberProceeding> _exMembersProceedingRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllProceedingExternalMembersQueryHandler(IResponseHelper responseHelper,
										  IGRepository<ExternalMember> exMembersRepo,
										  IMapper mapper,
										  IGRepository<Proceeding> proceedingRepo,
										  IGRepository<ExternalMemberProceeding> exMembersProceedingRepo)
		{
			_exMembersRepo = exMembersRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_proceedingRepo = proceedingRepo;
			_exMembersProceedingRepo = exMembersProceedingRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllProceedingExternalMembersQuery request,CancellationToken cancellationToken)
		{
			var proceeding = _proceedingRepo.GetAll(x => x.Id == request.ProceedingId).FirstOrDefault();

			if(proceeding == null)
			{
				return _responseHelper.NotFound("proceedingIsNotFound");
			}

			var externalMembersIds = _exMembersProceedingRepo.GetAll(x => x.ProceedingId == request.ProceedingId).Select(x => new { x.ExternalMemberId, x.IsAttend }).ToList();

			var externalMembers = _exMembersRepo.GetAll(x => externalMembersIds.Select(x => x.ExternalMemberId).Contains(x.Id)).Include(x => x.Permission).ToList();

			var externalMembersMapped = _mapper.Map<List<AllProceedingExternalMembersDto>>(externalMembers);

			externalMembersMapped.ForEach(x => x.IsAttend = externalMembersIds.Where(ex => ex.ExternalMemberId == x.Id).Select(x => x.IsAttend).FirstOrDefault());

			return _responseHelper.RetrievedSuccessfully(externalMembersMapped,"proceedingExternalMembersIsRetrievedSuccessfully");
		}
	}
}
