namespace Committees.Application.Services
{
	//[Authorize("ClientCredentialsPolicy")]
	public class MeetingService :MeetingProtoService.MeetingProtoServiceBase
	{
		private readonly IMediator _mediator;
		private readonly IMapper _mapper;
		private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

		public MeetingService(IMediator mediator,
						  IMapper mapper,
						  IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
		{
			_mediator = mediator;
			_mapper = mapper;
			_actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
		}
		public async override Task<ResponseAllMeetingAttachments> GetAllMeetingAttachments(RequestAllMeetingAttachments request,ServerCallContext context)
		{
			var allAttachments = await _mediator.Send(new GetAllMeetingAttachmentsQuery
			{
				MeetingId = Guid.TryParse(request.MeetingId,out Guid parsedId) ? parsedId : default(Guid)
			});

			var responseMapped = _mapper.Map<List<AllMeetingAttachmentProto>>(allAttachments.Result);

			var response = new ResponseAllMeetingAttachments();

			response.MeetingAttachment.AddRange(responseMapped);
			return await Task.FromResult(response);
		}
	}
}
