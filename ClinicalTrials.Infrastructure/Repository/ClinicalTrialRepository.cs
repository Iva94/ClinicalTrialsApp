using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using ClinicalTrials.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrials.Infrastructure.Repository
{
    /// <summary>
    /// Represents a repository for managing clinical trials.
    /// </summary>
    public class ClinicalTrialRepository : IClinicalTrialRepository
    {
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalTrialRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context instance.</param>
        public ClinicalTrialRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the clinical trial by specified identifier.
        /// </summary>
        /// <param name="clinicalTrialId">The clinical trial identifier.</param>
        /// <returns>The clinical trial with specified identifier.</returns>
        /// <exception cref="ArgumentNullException">Return exception if clinical trial identifier is null or empty</exception>
        public async Task<ClinicalTrial?> GetClinicalTrialByIdAsync(string clinicalTrialId)
        {
            if(string.IsNullOrWhiteSpace(clinicalTrialId))
            {
                throw new ArgumentNullException(nameof(clinicalTrialId));
            }

            return await _dbContext.ClinicalTrials.FindAsync(Guid.Parse(clinicalTrialId));
        }

        /// <summary>
        /// Gets the list of clinical trials filtered by tittle or status.
        /// </summary>
        /// <param name="tittle">The clinical trial tittle.</param>
        /// <param name="status">The clinical trial status.</param>
        /// <returns>List of clinical trials.</returns>
        public async Task<IEnumerable<ClinicalTrial>> GetClinicalTrialListAsync(string? tittle = null, string? status = null)
        {
            IQueryable<ClinicalTrial> query = _dbContext.ClinicalTrials.AsNoTracking();

            if (!string.IsNullOrEmpty(tittle))
            {
                query = query.Where(q => q.Title.StartsWith(tittle));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(q => q.Status.Equals(status));
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Creates a new clinical trial record in database.
        /// </summary>
        /// <param name="clinicalTrial">The clinical trial data.</param>
        /// <returns>Created clinical trial identifier.</returns>
        public async Task<Guid> CreateClinicalTrialAsync(ClinicalTrial clinicalTrial)
        {
            _dbContext.ClinicalTrials.Add(clinicalTrial);
            await _dbContext.SaveChangesAsync();
            return clinicalTrial.Id.Value;
        }
    }
}
