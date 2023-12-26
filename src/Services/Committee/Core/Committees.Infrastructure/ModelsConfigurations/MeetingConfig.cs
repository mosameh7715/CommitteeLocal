namespace Committees.Infrastructure.ModelsConfigurations
{
	public class MeetingConfig:IEntityTypeConfiguration<Meeting>
	{
		public void Configure(EntityTypeBuilder<Meeting> builder)
		{
			builder.HasMany(a => a.MeetingAttachments)
					.WithOne(a => a.Meeting)
					.HasForeignKey(a => a.MeetingId);

			builder.HasMany(a => a.InternalMemberMeetings)
					.WithOne(a => a.Meeting)
					.HasForeignKey(a => a.MeetingId);

			builder.HasMany(c => c.Proceedings)
					.WithMany(m => m.Meetings)
					.UsingEntity(j => j.ToTable("ProceedingMeetings"));

			builder.HasMany(c => c.Outputs)
					.WithMany(m => m.Meetings)
					.UsingEntity(j => j.ToTable("OutputMeetings"));

			builder.HasMany(c => c.ExternalMembers)
					.WithMany(m => m.Meetings)
					.UsingEntity(j => j.ToTable("ExternalMemberMeetings"));
		}
	}
}
