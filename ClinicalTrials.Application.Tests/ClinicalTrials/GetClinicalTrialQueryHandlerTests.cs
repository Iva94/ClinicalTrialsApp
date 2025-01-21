using ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrial;
using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using Moq;

namespace ClinicalTrials.Application.Tests.ClinicalTrials
{
    public class GetClinicalTrialQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ValidQuery_ReturnsClinicalTrialById()
        {
            var clinicalTrialId = new Guid();
            var clinicalTrialResult = new ClinicalTrial(clinicalTrialId, "ClinicalTrial Title", DateTime.Now.AddDays(-20), DateTime.Now, 1, ClinicalTrialStatus.Completed);
            var mockRepo = new Mock<IClinicalTrialRepository>();
            mockRepo.Setup(x => x.GetClinicalTrialByIdAsync(It.IsAny<string>())).ReturnsAsync(clinicalTrialResult);
            var handler = new GetClinicalTrialQueryHandler(mockRepo.Object);
            var command = new GetClinicalTrialQuery(new Guid().ToString());
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.Equal(command.ClinicalTrialId, result.Id.ToString());
        }
    }
}
