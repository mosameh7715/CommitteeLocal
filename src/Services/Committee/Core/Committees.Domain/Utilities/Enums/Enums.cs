namespace Domain.Enums
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }
	public enum ActionNameEnum
	{
		Get,
		Post,
		Put,
		Delete,
	}

	public enum CommitteeType
	{
		Committee,
		Council
	}
	public enum CommitteeMember
	{
		External,
		Internal
	}
	public enum CommitteeMemberUser
	{
		User,
		Role
	}
	public enum UserType
	{
		Local,
		Domain
	}
	public enum CommitteeTime
	{
		Always,
		Temporary,
		ProjectTemporary
	}
	public enum CommitteesStatus
	{
		Pending,
		Approved,
		Rejected
	}
	public enum CommitteeFormation
	{
		Internal,
		External
	}
	public enum CommitteeTypeInfo
	{
		Internal,
		External
	}
	public enum MeetingStatus
	{
		Remote,
		Onsite
	}
}