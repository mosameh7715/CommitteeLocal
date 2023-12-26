namespace Committees.Models
{
    public class Meeting : BaseEntity<Guid>
    {
        public string Rules { get; set; }
        public string Location { get; set; }
        public MeetingStatus MeetingStatus { get; set; }
        public bool HasExpertAssist { get; set; }
        public DateTime MeetingDate { get; set; }
        public Guid CommitteeId { get; set; }
        public Committee Committee { get; set; }
		public virtual ICollection<MeetingAttachment> MeetingAttachments { get; set; }
        public virtual ICollection<Proceeding> Proceedings { get; set; }
		public virtual ICollection<Output> Outputs { get; set; }
		public virtual ICollection<ExternalMember> ExternalMembers { get; set; }
        public virtual ICollection<InternalMemberMeeting> InternalMemberMeetings { get; set; }
    }
}
