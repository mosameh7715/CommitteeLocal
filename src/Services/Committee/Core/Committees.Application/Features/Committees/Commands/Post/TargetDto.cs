namespace Committees.Application.Features.Committees.Commands.Post
{
    public class TargetDto
    {
        public string Goal { get; set; }
        [JsonIgnore]
        public Guid CommitteeId { get; set; }
    }
}
