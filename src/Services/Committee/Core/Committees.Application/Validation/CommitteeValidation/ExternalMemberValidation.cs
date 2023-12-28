using Committees.Application.Features.Committees.Commands.Post;

namespace Committees.Application.Validation.CommitteeValidation
{
    public class ExternalMemberValidation : AbstractValidator<ExternalMemberDto>
    {
        public ExternalMemberValidation()
        {
            RuleFor(externalMember => externalMember.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(externalMember => externalMember.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required.")
                .Must(BeSaudiMobileNumber).WithMessage("PhoneNumber must be a valid Saudi mobile number");

            RuleFor(externalMember => externalMember.Job)
                .NotEmpty().WithMessage("Job is required.")
                .MaximumLength(255).WithMessage("Job cannot exceed 255 characters.");

            RuleFor(externalMember => externalMember.DestinationName)
                .NotEmpty().WithMessage("DestinationName is required.")
                .MaximumLength(255).WithMessage("DestinationName cannot exceed 255 characters.");

            RuleFor(externalMember => externalMember.Email)
                .NotEmpty().WithMessage("Email is required.")
                .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.")
                .Must(BeCustomEmail).WithMessage("Invalid email address.");

            RuleFor(externalMember => externalMember.PermissionId)
                .NotEmpty().WithMessage("PermissionId is required.");

            RuleFor(externalMember => externalMember.CommitteeId)
                .NotEmpty().WithMessage("CommitteeId is required.");
        }
        private bool BeSaudiMobileNumber(string phoneNumber)
        {
            var saudiMobileRegex = new Regex(@"^\+9665\d{8}$");
            return saudiMobileRegex.IsMatch(phoneNumber);
        }

        private bool BeCustomEmail(string email)
        {
            var customEmailRegex = new Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return customEmailRegex.IsMatch(email);
        }
    }
}
