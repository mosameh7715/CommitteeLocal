namespace Committees.Application.Validation.CommitteeValidation
{
    public class PostCommitteeValidation : AbstractValidator<PostCommitteeDto>
    {
        public PostCommitteeValidation()
        {
            RuleFor(committee => committee.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(committee => committee.ProjectName)
                .NotEmpty().WithMessage("ProjectName is required.")
                .MaximumLength(255).WithMessage("ProjectName cannot exceed 255 characters.");

            RuleFor(committee => committee.WorkRule)
                .NotEmpty().WithMessage("WorkRule is required.")
                .MaximumLength(1000).WithMessage("WorkRule cannot exceed 1000 characters.");

            RuleFor(committee => committee.Missions)
                .NotEmpty().WithMessage("Missions is required.")
                .MaximumLength(1000).WithMessage("Missions cannot exceed 1000 characters.");

            RuleFor(committee => committee.Location)
                .NotEmpty().WithMessage("Location is required.")
                .MaximumLength(255).WithMessage("Location cannot exceed 255 characters.");

            RuleFor(committee => committee.Position)
                .NotEmpty().WithMessage("Position is required.")
                .MaximumLength(255).WithMessage("Position cannot exceed 255 characters.");

            RuleFor(committee => committee.Attachments)
                .Cascade(CascadeMode.Stop)
                .Must(files => files == null || files.All(file => IsValidFile(file)))
                .WithMessage("All attachments must be valid files.");

            RuleFor(committee => committee.WorkRules)
                .Cascade(CascadeMode.Stop)
                .Must(files => files == null || files.All(file => IsValidFile(file)))
                .WithMessage("All attachments must be valid files.");

            RuleForEach(committee => committee.ExternalMembers)
                .SetValidator(new ExternalMemberValidation());

            RuleFor(committee => committee.LegalDocument)
                .NotEmpty().When(committee => committee.HasLegalDocument)
                .WithMessage("LegalDocument is required when HasLegalDocument is true.")
                .MaximumLength(1000).When(committee => committee.HasLegalDocument)
                .WithMessage("LegalDocument cannot exceed 1000 characters when HasLegalDocument is true.");
        }
        private bool IsValidFile(IFormFile file)
        {
            if (file == null)
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".pdf", ".doc", ".docx", ".mp4", ".avi", ".mov", ".wmv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return allowedExtensions.Contains(fileExtension);
        }
    }
}
