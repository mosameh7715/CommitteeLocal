﻿namespace Committees.Application.Features.Committees.Queries.GetCommitteeTimeTable
{
    public class MeetingDTO
    {
        public Guid Id { get; set; }
        public string Rules { get; set; }
        public DateTime MeetingDate { get; set; }
        public Guid CommitteeId { get; set; }
    }
}
