using MediatR;

namespace ClinicalTrials.Application.Commands.ClinicalTrials.UploadClinicalTrial
{
    /// <summary>
    /// Represents an upload clinical trial command.
    /// </summary>
    public sealed class UploadClinicalTrialCommand : IRequest<string>
    {
        /// <summary>
        /// Gets or sets the clinical trial identifier.
        /// </summary>
        public string ClinicalTrialId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the participants.
        /// </summary>
        public int? Participants { get; set; }

        /// <summary>
        /// Gets or sets the clinical trial status. Status can be: Not Started, Ongoing, Completed.
        /// </summary>
        public string? Status { get; set; }
    }
}
