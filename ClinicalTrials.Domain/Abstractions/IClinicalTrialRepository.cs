using ClinicalTrials.Domain.ClinicalTrials;

namespace ClinicalTrials.Domain.Abstractions
{
    public interface IClinicalTrialRepository
    {
        Task<ClinicalTrial> GetClinicalTrialByIdAsync(string id);
        Task<IEnumerable<ClinicalTrial>> GetClinicalTrialListAsync(string tittle = null, string status = null);
        Task<Guid> CreateClinicalTrialAsync(ClinicalTrial clinicalTrial);
    }
}
