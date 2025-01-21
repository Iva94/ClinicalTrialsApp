using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using MediatR;

namespace ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrialList
{
    public class GetClinicalTrialListQueryHandler : IRequestHandler<GetClinicalTrialListQuery, IEnumerable<ClinicalTrial>>
    {
        private readonly IClinicalTrialRepository _clinicalTrialRepository;

        public GetClinicalTrialListQueryHandler(IClinicalTrialRepository clinicalTrialRepository)
        {
            _clinicalTrialRepository = clinicalTrialRepository;
        }

        public async Task<IEnumerable<ClinicalTrial>> Handle(GetClinicalTrialListQuery request, CancellationToken cancellationToken)
        {
            return await _clinicalTrialRepository.GetClinicalTrialListAsync(request.Title, request.Status);
        }
    }
}
