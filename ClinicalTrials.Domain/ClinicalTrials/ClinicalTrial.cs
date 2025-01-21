using ClinicalTrials.Domain.Abstractions;

namespace ClinicalTrials.Domain.ClinicalTrials
{
    /// <summary>
    /// Represents a clinical trial.
    /// </summary>
    public  sealed class ClinicalTrial : Entity
    {
        /// <summary>
        /// Get or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get or sets the start date.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Get or sets the end date. Default value is one month from the start date.
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Get or sets the duration of the clinical trial in days.
        /// </summary>
        public double Duration {  get; set; }

        /// <summary>
        /// Get or sets the participants.
        /// </summary>
        public int? Participants { get; set; }

        /// <summary>
        /// Get or sets the clinical trial status. Status can be: Not Started, Ongoing, Completed.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalTrial"/> class.
        /// </summary>
        public ClinicalTrial(Guid? id, string title, DateTime startDate, DateTime? endDate, int? participants, string status) 
            : base(id)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate ?? startDate.AddDays(30);
            Duration = (EndDate - StartDate).Value.TotalDays;
            Participants = participants;
            Status = status ?? ClinicalTrialStatus.Ongoing;
        }
    }
}
