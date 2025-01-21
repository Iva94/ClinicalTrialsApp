using FluentValidation;

namespace ClinicalTrials.Application.Commands.ClinicalTrials.UploadClinicalTrial
{
    internal class UploadClinicalTrialCommandValidator : AbstractValidator<UploadClinicalTrialCommand>
    {
        public UploadClinicalTrialCommandValidator()
        {
            RuleFor(c => c.ClinicalTrialId).NotNull().NotEmpty();
            RuleFor(c => c.Title).NotNull().NotEmpty();
            RuleFor(c => c.StartDate).NotNull().NotEmpty();
            RuleFor(c => c.Status).NotNull().NotEmpty();
        }
    }
}
