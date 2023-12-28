namespace Committees.Application.Services
{
	//[Authorize("ClientCredentialsPolicy")]
	public class OutputService :OutputProtoService.OutputProtoServiceBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public OutputService(IMediator mediator,
						  IMapper mapper,
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_mediator = mediator;
			_mapper = mapper;
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<ResponseAllOutputAttachments> GetAllOutputAttachments(RequestAllOutputAttachments request,ServerCallContext context)
		{
			var allAttachments = await _mediator.Send(new GetAllOutputAttachmentsQuery
			{
				OutputId = Guid.TryParse(request.OutputId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<AllOutputAttachmentProto>>(allAttachments.Result);

			var response = new ResponseAllOutputAttachments();

			response.OutputAttachment.AddRange(responseMapped);
			return await Task.FromResult(response);
		}
	}
}
