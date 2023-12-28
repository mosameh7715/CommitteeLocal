
namespace Committees.Application.Features.Committees.Queries.GetAllInternalMembers
{
	public class GetAllInternalMembersQueryHandler:IRequestHandler<GetAllInternalMembersQuery,ResponseDTO>
	{
		private readonly IGRepository<InternalMember> _inMembersRepo;
		private readonly IGRepository<CommitteeInternalMember> _committeeInMembersRepo;
		private readonly IGRepository<Committee> _committeeRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllInternalMembersQueryHandler(IResponseHelper responseHelper,
										  IGRepository<InternalMember> inMembersRepo,
										  IMapper mapper,
										  IGRepository<Committee> committeeRepo,
										  IGRepository<CommitteeInternalMember> committeeInMembersRepo)
		{
			_inMembersRepo = inMembersRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_committeeRepo = committeeRepo;
			_committeeInMembersRepo = committeeInMembersRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllInternalMembersQuery request,CancellationToken cancellationToken)
		{
			var committee = _committeeRepo.GetAll(x => x.Id == request.CommitteeId).FirstOrDefault();

			if(committee == null)
			{
				return _responseHelper.NotFound("committeeIsNotFound");
			}

			var internalMemberIds = _committeeInMembersRepo.GetAll(x => x.CommitteeId == request.CommitteeId).Select(x => x.InternalMemberId).ToList();

			var internalMembers = _inMembersRepo.GetAll(x => internalMemberIds.Contains(x.UserId)).Include(x => x.Permission).ToList(); 

			var internalMembersMapped = _mapper.Map<List<AllExternalMembersDto>>(internalMembers);


			return _responseHelper.RetrievedSuccessfully(internalMembersMapped,"internalMembersIsRetrievedSuccessfully");
		}
	}
}
