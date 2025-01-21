using ClinicalTrials.Domain.ClinicalTrials;
using MediatR;

namespace ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrial
{
    /// <summary>
    /// Represents the query for getting clinical trial by identifier.
    /// </summary>
    /// <param name="ClinicalTrialId"></param>
    public sealed record GetClinicalTrialQuery(string ClinicalTrialId) : IRequest<ClinicalTrial>;
}
