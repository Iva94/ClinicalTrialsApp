using ClinicalTrials.Domain.ClinicalTrials;
using MediatR;

namespace ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrialList
{
    /// <summary>
    /// Represents the query for getting clinical trials.
    /// </summary>
    public sealed class GetClinicalTrialListQuery : IRequest<IEnumerable<ClinicalTrial>>
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public string? Status { get; set; }
    }
}
