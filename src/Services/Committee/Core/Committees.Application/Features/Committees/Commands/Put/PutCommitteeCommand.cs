﻿namespace Committees.Application.Features.Committees.Commands.Put
{
    public class PutCommitteeCommand : IRequest<ResponseDTO>
    {
        public Guid CommitteeId { get; set; }
        public PutCommitteeDto CommitteeDto { get; set; }
    }
}
