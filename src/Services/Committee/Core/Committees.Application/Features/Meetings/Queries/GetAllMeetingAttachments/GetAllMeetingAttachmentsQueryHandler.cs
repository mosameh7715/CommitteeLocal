namespace Committees.Application.Features.Meetings.Queries.GetAllMeetingAttachments
{
	public class GetAllMeetingAttachmentsQueryHandler:IRequestHandler<GetAllMeetingAttachmentsQuery,ResponseDTO>
	{
		private readonly IGRepository<MeetingAttachment> _attachmentRepo;
		private readonly IGRepository<Meeting> _meetingRepo;
		private readonly IResponseHelper _responseHelper;
		private readonly IMapper _mapper;

		public GetAllMeetingAttachmentsQueryHandler(IResponseHelper responseHelper,
										  IGRepository<MeetingAttachment> attachmentRepo,
										  IMapper mapper,
										  IGRepository<Meeting> meetingRepo)
		{
			_attachmentRepo = attachmentRepo;
			_responseHelper = responseHelper;
			_mapper = mapper;
			_meetingRepo = meetingRepo;
		}
		public async Task<ResponseDTO> Handle(GetAllMeetingAttachmentsQuery request,CancellationToken cancellationToken)
		{
			var meeting = _meetingRepo.GetAll(x => x.Id == request.MeetingId).FirstOrDefault();

            if (meeting == null)
            {
				return _responseHelper.NotFound("meetingIsNotFound");
			}

			var attachment = _attachmentRepo.GetAll(x => x.MeetingId == request.MeetingId).ToList();

			var attachmentsMapped = _mapper.Map<List<MeetingAttachmentDto>>(attachment);


			return _responseHelper.RetrievedSuccessfully(attachmentsMapped,"meetingAttachmentsIsRetrievedSuccessfully");
		}
	}
}
