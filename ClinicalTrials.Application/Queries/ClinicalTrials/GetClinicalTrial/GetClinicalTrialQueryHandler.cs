using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using MediatR;

namespace ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrial
{
    public sealed class GetClinicalTrialQueryHandler : IRequestHandler<GetClinicalTrialQuery, ClinicalTrial>
    {
        private readonly IClinicalTrialRepository _clinicalTrialRepository;

        public GetClinicalTrialQueryHandler(IClinicalTrialRepository clinicalTrialRepository)
        {
            _clinicalTrialRepository = clinicalTrialRepository;
        }
        public Task<ClinicalTrial> Handle(GetClinicalTrialQuery request, CancellationToken cancellationToken)
        {
            return _clinicalTrialRepository.GetClinicalTrialByIdAsync(request.ClinicalTrialId);
        }
    }
}
