using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using MediatR;

namespace ClinicalTrials.Application.Commands.ClinicalTrials.UploadClinicalTrial
{
    public sealed class UploadClinicalTrialCommandHandler : IRequestHandler<UploadClinicalTrialCommand, string>
    {
        private readonly IClinicalTrialRepository _clinicalTrialRepository;

        public UploadClinicalTrialCommandHandler(IClinicalTrialRepository clinicalTrialRepository)
        {
            _clinicalTrialRepository = clinicalTrialRepository;
        }

        public async Task<string> Handle(UploadClinicalTrialCommand request, CancellationToken cancellationToken)
        {
            Guid clinicalTrialId;
            if (!Guid.TryParse(request.ClinicalTrialId, out clinicalTrialId)) {
                throw new ArgumentException("Invalid identifier.");
            }

            var clinicalTrial = new ClinicalTrial(
                clinicalTrialId,
                request.Title,
                request.StartDate,
                request.EndDate,
                request.Participants,
                request.Status);

            await _clinicalTrialRepository.CreateClinicalTrialAsync(clinicalTrial);
            return clinicalTrialId.ToString();
        }
    }
}
