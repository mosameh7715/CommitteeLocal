namespace Committees.Application.Features.MeetingsFeatures.Command.Post
{
    public class PostMeetingCommandHandler : IRequestHandler<PostMeetingCommand, ResponseDTO>
    {
        private readonly IGRepository<Meeting> _meetingRepo;
        private readonly IGRepository<MeetingAttachment> _meetingAttachmentRepo;
        private readonly IMapper _mapper;
        private readonly IResponseHelper _responseHelper;
        private readonly ResponseDTO _responseDto;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hosting;
        public Guid _loggedInUserId;

        public PostMeetingCommandHandler
        (
            IGRepository<Meeting> meetingRepo,
            IGRepository<MeetingAttachment> meetingAttachmentRepo,
            IHttpContextAccessor _httpContextAccessor,
            IMapper mapper,
            IResponseHelper responseHelper,
            IUnitOfWork unitOfWork,
            IWebHostEnvironment hosting
        )
        {
            _meetingRepo = meetingRepo;
            _meetingAttachmentRepo = meetingAttachmentRepo;
            _mapper = mapper;
            _responseHelper = responseHelper;
            _responseDto = new ResponseDTO();
            _unitOfWork = unitOfWork;
            _hosting = hosting;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(PostMeetingCommand request, CancellationToken cancellationToken)
        {
            var validator = new PostMeetingValidation();
            var validation = validator.Validate(request.MeetingDto);

            if (!validation.IsValid)
            {
                _responseDto.Result = null;
                _responseDto.StatusEnum = StatusEnum.Exception;
                _responseDto.Message = JsonSerializer.Serialize(validation.Errors.Select(n => n.ErrorMessage).ToList());
                return _responseDto;
            }

            var meetingToAdd = _mapper.Map<Meeting>(request.MeetingDto);
            meetingToAdd.CreatedBy = _loggedInUserId;

            _meetingRepo.Add(meetingToAdd);

            if (request.MeetingDto.MeetingAttachments != null)
            {
                var meetingAttachments = await Task.WhenAll(
                    request.MeetingDto.MeetingAttachments.Select(async att =>
                    {
                        var file = await Upload.UploadFiles(att, _hosting, meetingToAdd.Id.ToString() + "MeetingAttachments");
                        return new MeetingAttachment
                        {
                            MeetingId = meetingToAdd.Id,
                            Path = file,
                            CreatedBy = _loggedInUserId
                        };
                    })
                );

                _meetingAttachmentRepo.AddRange(meetingAttachments);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("MeetingAddedSuccessfully!");
        }
    }
}
