namespace Committees.Application.Features.Committees.Queries.GetAllExternalMembers
{
	public class GetAllExternalMembersQueryHandler:IRequestHandler<GetAllExternalMembersQuery,ResponseDTO>
	{
		private readonly IGRepository<ExternalMember> _exMembersRepo;
		private readonly IGRepository<Committee> _committeeRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllExternalMembersQueryHandler(IResponseHelper responseHelper,
										  IGRepository<ExternalMember> exMembersRepo,
										  IMapper mapper,
										  IGRepository<Committee> committeeRepo)
		{
			_exMembersRepo = exMembersRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_committeeRepo = committeeRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllExternalMembersQuery request,CancellationToken cancellationToken)
		{
			var committee = _committeeRepo.GetAll(x => x.Id == request.CommitteeId).FirstOrDefault();

			if(committee == null)
			{
				return _responseHelper.NotFound("committeeIsNotFound");
			}

			var externalMembers = _exMembersRepo.GetAll(x => x.CommitteeId == request.CommitteeId).Include(x => x.Permission).ToList();

			var externalMembersMapped = _mapper.Map<List<AllExternalMembersDto>>(externalMembers);


			return _responseHelper.RetrievedSuccessfully(externalMembersMapped,"externalMembersIsRetrievedSuccessfully");
		}
	}
}
