namespace Committees.Application.Features.ProceedingFeatures.Command.PostProceedingMembers
{
    public class PostProceedingMembersHandler : IRequestHandler<PostProceedingMembersCommand, ResponseDTO>
    {
        private readonly IGRepository<InternalMemberProceeding> _internalMemberProceedingRepo;
        private readonly IGRepository<ExternalMemberProceeding> _externalMemberProceedingRepo;
        private readonly IGRepository<Proceeding> _proceedingRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IResponseHelper _responseHelper;
        private readonly ResponseDTO _responseDto;

        public PostProceedingMembersHandler
        (
            IGRepository<InternalMemberProceeding> internalMemberProceedingRepo,
            IGRepository<ExternalMemberProceeding> externalMemberProceedingRepo,
            IGRepository<Proceeding> proceedingRepo,
            IUnitOfWork unitOfWork,
            IResponseHelper responseHelper
        )
        {
            _internalMemberProceedingRepo = internalMemberProceedingRepo;
            _externalMemberProceedingRepo = externalMemberProceedingRepo;
            _proceedingRepo = proceedingRepo;
            _unitOfWork = unitOfWork;
            _responseHelper = responseHelper;
            _responseDto = new ResponseDTO();
        }

        public async Task<ResponseDTO> Handle(PostProceedingMembersCommand request, CancellationToken cancellationToken)
        {
            var proceeding = await _proceedingRepo.GetFirstAsync(x => x.Id == request.ProceedingId);
            if (proceeding == null)
            {
                return _responseHelper.NotFound("ProceedingNotFound!");
            }

            if (request.MembersDto.InternalMemberIds != null && request.MembersDto.InternalMemberIds.Any())
            {
                var internalMembers = request.MembersDto.InternalMemberIds.Select(internalMemberId => new InternalMemberProceeding
                {
                    InternalMemberId = internalMemberId,
                    ProceedingId = request.ProceedingId,
                    IsAttend = true
                });

                _internalMemberProceedingRepo.AddRange(internalMembers);
            }

            // Add external members
            if (request.MembersDto.ExternalMemberIds != null && request.MembersDto.ExternalMemberIds.Any())
            {
                var externalMembers = request.MembersDto.ExternalMemberIds.Select(externalMemberId => new ExternalMemberProceeding
                {
                    ExternalMemberId = externalMemberId,
                    ProceedingId = request.ProceedingId,
                    IsAttend = true
                });

                _externalMemberProceedingRepo.AddRange(externalMembers);
            }

            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("MembersAssignedSuccessfully!");
        }
    }
}
