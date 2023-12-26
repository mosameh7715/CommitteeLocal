namespace Committees.Application.Validation.OutputValidation
{
    public class PostOutputTypeValidator : AbstractValidator<PostOutputTypeCommand>
    {
        public PostOutputTypeValidator()
        {
            RuleFor(x => x.NameAr)
                .NotEmpty().WithMessage("NameAr is required")
                .Must(BeArabic).WithMessage("NameAr must be in Arabic language");

            RuleFor(x => x.NameEn)
                .NotEmpty().WithMessage("NameEn is required")
                .Must(BeEnglish).WithMessage("NameEn must be in English language");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.CSSClasses)
                .NotEmpty().WithMessage("CSSClasses is required");
        }

        private bool BeArabic(string name)
        {
            var arabicRegex = new Regex(@"^[\p{IsArabic}\s]+$");
            return arabicRegex.IsMatch(name);
        }
        private bool BeEnglish(string name)
        {
            var englishRegex = new Regex(@"^[a-zA-Z\s]+$");
            return englishRegex.IsMatch(name);
        }
    }

}
