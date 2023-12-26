namespace Committees.Infrastructure.ModelsConfigurations
{
	public class InternalMemberConfig:IEntityTypeConfiguration<InternalMember>
	{
		public void Configure(EntityTypeBuilder<InternalMember> builder)
		{
			builder.HasMany(a => a.InternalMemberMeetings)
					.WithOne(a => a.InternalMember)
					.HasForeignKey(a => a.InternalMemberId);


			builder.HasMany(a => a.InternalMemberProceedings)
					.WithOne(a => a.InternalMember)
					.HasForeignKey(a => a.InternalMemberId);
		}
	}
}
