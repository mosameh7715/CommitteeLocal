using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Committees.Application.Features.Committees.Commands.UpdateCommitteesStatus
{
    public class UpdateCommitteesStatusHandler : IRequestHandler<UpdateCommitteesStatusCommand, ResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGRepository<Committee> _committeeRepository;
        private readonly IResponseHelper _responseHelper;
        private readonly ILogger<UpdateCommitteesStatusHandler> _logger;
        public Guid _loggedInUserId;

        public UpdateCommitteesStatusHandler
        (
            IUnitOfWork unitOfWork,
            IGRepository<Committee> committeeRepository,
            ILogger<UpdateCommitteesStatusHandler> logger,
            IHttpContextAccessor _httpContextAccessor,
            IResponseHelper responseHelper
        )
        {
            _unitOfWork = unitOfWork;
            _committeeRepository = committeeRepository;
            _responseHelper = responseHelper;
            _logger = logger;
            _loggedInUserId = Infrastructure.Helpers.LoggedInUserProvider.GetLoggedInUserId(_httpContextAccessor);
        }

        public async Task<ResponseDTO> Handle(UpdateCommitteesStatusCommand request, CancellationToken cancellationToken)
        {
            var committeeToUpdate = await _committeeRepository.GetFirstAsync(x => x.Id == request.CommitteeId);

            if (committeeToUpdate == null)
            {
                return _responseHelper.NotFound("CommitteeNotFound!");
            }

            committeeToUpdate.CommitteesStatus = request.CommitteesStatus;
            committeeToUpdate.GroupNotes = request.GroupNotes;
            committeeToUpdate.UpdatedBy = _loggedInUserId;

            _committeeRepository.Update(committeeToUpdate);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();

            return _responseHelper.SavedSuccessfully("CommitteesStatusUpdatedSuccessfully!");
        }
    }
}
