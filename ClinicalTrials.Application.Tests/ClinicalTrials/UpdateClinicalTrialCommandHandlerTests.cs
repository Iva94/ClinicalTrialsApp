using ClinicalTrials.Application.Commands.ClinicalTrials.UploadClinicalTrial;
using ClinicalTrials.Domain.Abstractions;
using ClinicalTrials.Domain.ClinicalTrials;
using Moq;

namespace ClinicalTrials.Application.Tests.ClinicalTrials
{
    public class UpdateClinicalTrialCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsClinicalTrialId()
        {
            var clinicalTrialId = new Guid();
            var mockRepo = new Mock<IClinicalTrialRepository>();
            mockRepo.Setup(x => x.CreateClinicalTrialAsync(It.IsAny<ClinicalTrial>())).ReturnsAsync(clinicalTrialId);
            var handler = new UploadClinicalTrialCommandHandler(mockRepo.Object);
            var command = new UploadClinicalTrialCommand
            {
                ClinicalTrialId = clinicalTrialId.ToString(),
                Title = "ClinicalTrial Title",
                StartDate = DateTime.Now.AddDays(-20),
                EndDate = DateTime.Now,
                Participants = 4,
                Status = ClinicalTrialStatus.Ongoing
            };

            var result = await handler.Handle(command, CancellationToken.None);
            Assert.Equal(command.ClinicalTrialId, result);
        }
    }
}