using ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrialList;
using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using Moq;

namespace ClinicalTrials.Application.Tests.ClinicalTrials
{
    public class GetClinicalTrialListQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ReturnsListOfClinicalTrials()
        {
            var clinicalTrialResults = new List<ClinicalTrial>
            {
                new ClinicalTrial(new Guid(), "ClinicalTrial Title 1", DateTime.Now.AddDays(-30), DateTime.Now, 1, ClinicalTrialStatus.Completed)
            };

            var mockRepo = new Mock<IClinicalTrialRepository>();
            mockRepo.Setup(x => x.GetClinicalTrialListAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(clinicalTrialResults);
            var handler = new GetClinicalTrialListQueryHandler(mockRepo.Object);
            var command = new GetClinicalTrialListQuery
            {
                Title = "ClinicalTrial Title 1",
            };
            var results = await handler.Handle(command, CancellationToken.None);
            
            Assert.NotNull(results);
            Assert.True(results.Count() == 1);
            Assert.Equal(clinicalTrialResults[0].Id, results.FirstOrDefault()?.Id);
        }
    }
}
