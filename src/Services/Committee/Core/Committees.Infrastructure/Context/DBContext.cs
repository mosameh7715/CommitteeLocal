namespace Committees.Infrastructure.Context
{
    public class DBContext : DbContext
    {

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
        public DbSet<Committee> Committees { get; set; }
		public DbSet<CommitteeAttachment> CommitteeAttachments { get; set; }
        public DbSet<ExternalMember> ExternalMembers { get; set; }
        public DbSet<InternalMember> InternalMembers { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingAttachment> MeetingAttachments { get; set; }
		public DbSet<Output> Outputs { get; set; }
		public DbSet<OutputAttachment> OutputAttachments { get; set; }
		public DbSet<OutputType> OutputTypes { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Proceeding> Proceedings { get; set; }
		public DbSet<ProceedingAttachment> ProceedingAttachments { get; set; }
		public DbSet<Target> Targets { get; set; }
		public DbSet<WorkRule> WorkRules { get; set; }
		public DbSet<InternalMemberMeeting> InternalMemberMeetings { get; set; }
		public DbSet<InternalMemberProceeding> InternalMemberProceedings { get; set; }


		public bool AllMigrationsApplied()
        {
            var applied = this.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            var total = this.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        public void Migrate()
        {
            this.Database.Migrate();
        }
    }
}