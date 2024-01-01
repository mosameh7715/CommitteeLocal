namespace Committees.Application.Features.Committees.Commands.Post
{
    public class ExternalMemberDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Job { get; set; }
        public string DestinationName { get; set; }
        public string Email { get; set; }
        public Guid PermissionId { get; set; }
        [JsonIgnore]
        public Guid CommitteeId { get; set; }
    }
}
