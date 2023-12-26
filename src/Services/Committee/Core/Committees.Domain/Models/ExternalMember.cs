namespace Committees.Models
{
    public class ExternalMember : BaseEntity<Guid>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public string DestinationName { get; set; }
        public string Email { get; set; }
		public Guid PermissionId { get; set; }
		public Permission Permission { get; set; }
		public Guid CommitteeId { get; set; }
		public Committee Committee { get; set; }
        public ICollection<Proceeding> Proceedings { get; set; }
		public ICollection<Meeting> Meetings { get; set; }
	}
}
