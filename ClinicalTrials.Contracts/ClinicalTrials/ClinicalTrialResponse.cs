namespace ClinicalTrials.Contracts.ClinicalTrials
{
    /// <summary>
    /// Represents the response model of a clinical trial.
    /// </summary>
    public class ClinicalTrialResponse
    {
        /// <summary>
        /// Gets the clinical trial ID.
        /// </summary>
        public string TrialId { get; set; }

        /// <summary>
        /// Gets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets the end date. Default value is one month from the start date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets the duration of the clinical trial in days.
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Gets the participants.
        /// </summary>
        public int? Participants { get; set; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public string Status { get; set; }
    }
}
