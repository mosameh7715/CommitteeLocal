using Microsoft.EntityFrameworkCore;

namespace Committees.Infrastructure.ModelsConfigurations
{
	public class InternalMemberMeetingConfig:IEntityTypeConfiguration<InternalMemberMeeting>
	{
		public void Configure(EntityTypeBuilder<InternalMemberMeeting> builder)
		{
			builder
				.HasKey(x => new { x.InternalMemberId,x.MeetingId });
		}
	}
}
