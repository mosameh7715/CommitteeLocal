namespace Committees.Application.Features.Proceedings.GetAllInternalMembers
{
	public class GetAllProceedingInternalMembersQueryHandler:IRequestHandler<GetAllProceedingInternalMembersQuery,ResponseDTO>
	{
		private readonly IGRepository<InternalMember> _inMembersRepo;
		private readonly IGRepository<Proceeding> _proceedingRepo;
		private readonly IGRepository<InternalMemberProceeding> _inMembersProceedingRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllProceedingInternalMembersQueryHandler(IResponseHelper responseHelper,
										  IGRepository<InternalMember> inMembersRepo,
										  IMapper mapper,
										  IGRepository<Proceeding> proceedingRepo,
										  IGRepository<InternalMemberProceeding> inMembersProceedingRepo)
		{
			_inMembersRepo = inMembersRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_proceedingRepo = proceedingRepo;
			_inMembersProceedingRepo = inMembersProceedingRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllProceedingInternalMembersQuery request,CancellationToken cancellationToken)
		{
			var proceeding = _proceedingRepo.GetAll(x => x.Id == request.ProceedingId).FirstOrDefault();

			if(proceeding == null)
			{
				return _responseHelper.NotFound("proceedingIsNotFound");
			}

			var internalMembersIds = _inMembersProceedingRepo.GetAll(x => x.ProceedingId == request.ProceedingId).Select(x => new { x.InternalMemberId,x.IsAttend }).ToList();

			var internalMembers = _inMembersRepo.GetAll(x => internalMembersIds.Select(x => x.InternalMemberId).Contains(x.UserId)).Include(x => x.Permission).ToList();

			var internalMembersMapped = _mapper.Map<List<AllProceedingInternalMembersDto>>(internalMembers);

			internalMembersMapped.ForEach(x => x.IsAttend = internalMembersIds.Where(ex => ex.InternalMemberId == x.UserId).Select(x => x.IsAttend).FirstOrDefault());

			return _responseHelper.RetrievedSuccessfully(internalMembersMapped,"proceedingInternalMembersIsRetrievedSuccessfully");
		}
	}
}
