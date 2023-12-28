namespace Committees.Infrastructure.ModelsConfigurations
{
	public class CommitteeConfig:IEntityTypeConfiguration<Committee>
	{
		public void Configure(EntityTypeBuilder<Committee> builder)
		{
			builder.HasMany(a => a.Meetings)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);

			builder.HasMany(a => a.Attachments)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);

			builder.HasMany(a => a.WorkRules)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);

			builder.HasMany(a => a.ExternalMembers)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);


			builder.HasMany(a => a.Proceedings)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);

			builder.HasMany(a => a.Outputs)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);

			builder.HasMany(a => a.Targets)
					.WithOne(a => a.Committee)
					.HasForeignKey(a => a.CommitteeId);
		}
	}
}
