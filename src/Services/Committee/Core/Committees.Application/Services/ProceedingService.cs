namespace Committees.Application.Services
{
	//[Authorize("ClientCredentialsPolicy")]
	public class ProceedingService:ProceedingProtoService.ProceedingProtoServiceBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public ProceedingService(IMediator mediator,
						  IMapper mapper,
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_mediator = mediator;
			_mapper = mapper;
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<ResponseAllProceedingExternalMembers> GetAllProceedingExternalMembers(RequestAllProceedingExternalMembers request,ServerCallContext context)
		{
			var allExternalMembers = await _mediator.Send(new GetAllProceedingExternalMembersQuery
			{
				ProceedingId = Guid.TryParse(request.ProceedingId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<ProceedingExternalMemberProto>>(allExternalMembers.Result);

			var response = new ResponseAllProceedingExternalMembers();

			response.ExternalMember.AddRange(responseMapped);

			return await Task.FromResult(response);
		}

		public async override Task<ResponseAllProceedingInternalMembers> GetAllProceedingInternalMembers(RequestAllProceedingInternalMembers request,ServerCallContext context)
		{
			var allInternalMembers = await _mediator.Send(new GetAllProceedingInternalMembersQuery
			{
				ProceedingId = Guid.TryParse(request.ProceedingId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<ProceedingInternalMemberProto>>(allInternalMembers.Result);

			var response = new ResponseAllProceedingInternalMembers();

			response.InternalMember.AddRange(responseMapped);

			return await Task.FromResult(response);
		}

		public async override Task<ResponseGetProceedingById> GetProceedingById(RequestGetProceedingById request,ServerCallContext context)
		{
			var proceeding = await _mediator.Send(new GetProceedingByIdQuery { ProceedingId = Guid.TryParse(request.ProceedingId,out Guid parsedId) ? parsedId : default(Guid) });

			var responseMapped = _mapper.Map<ProceedingByIdProto>(proceeding.Result);

			var response = new ResponseGetProceedingById();

			response.Proceeding = responseMapped;

			return await Task.FromResult(response);
		}
	}
}
