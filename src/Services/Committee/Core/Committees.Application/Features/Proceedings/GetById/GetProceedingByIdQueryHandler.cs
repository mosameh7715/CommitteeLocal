namespace Committees.Application.Features.Proceedings.GetById
{
	public class GetProceedingByIdQueryHandler:IRequestHandler<GetProceedingByIdQuery,ResponseDTO>
	{
		private readonly IGRepository<Proceeding> _proceedingRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetProceedingByIdQueryHandler(IResponseHelper responseHelper,
										  IGRepository<Proceeding> proceedingRepo,
										  IMapper mapper)
		{
			_proceedingRepo = proceedingRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
		}
		public async Task<ResponseDTO> Handle(GetProceedingByIdQuery request,CancellationToken cancellationToken)
		{
			var proceeding = _proceedingRepo.GetAll(x => x.Id == request.ProceedingId)
											  .Include(x => x.ExternalMemberProceedings.Where(c => c.State == State.NotDeleted))
											  .ThenInclude(x => x.ExternalMember)
											  .Include(x => x.InternalMemberProceedings.Where(a => a.State == State.NotDeleted))
											  .ThenInclude(x => x.InternalMember)
											  .Include(x => x.ProceedingAttachments.Where(a => a.State == State.NotDeleted))
											  .FirstOrDefault();

			if(proceeding == null)
			{
				return _responseHelper.NotFound("proceedingIsNotExists");
			}

			var proceedingMapped = _mapper.Map<GetProceedingByIdDto>(proceeding);

			return _responseHelper.RetrievedSuccessfully(proceedingMapped,"proceedingIsRetrievedSuccessfully");
		}
	}
}
