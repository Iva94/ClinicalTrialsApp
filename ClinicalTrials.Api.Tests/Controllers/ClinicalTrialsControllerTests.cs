using ClinicalTrials.Api.Controllers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace ClinicalTrials.Api.Tests.Controllers
{
    public class ClinicalTrialsControllerTests
    {
        private readonly Mock<ILogger<ClinicalTrialsController>> _mockLogger;
        private readonly Mock<IMediator> _mockMediator;
        private readonly IConfiguration _configuration;
        private readonly ClinicalTrialsController _controller;

        public ClinicalTrialsControllerTests()
        {
            var inMemorySettings = new Dictionary<string, string> {{"RequestSizeLimit", "1000000"}};

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            _mockLogger = new Mock<ILogger<ClinicalTrialsController>>();
            _mockMediator = new Mock<IMediator>();
            _controller = new ClinicalTrialsController(_mockLogger.Object, _mockMediator.Object, _configuration);
        }

        [Fact]
        public async Task UploadFile_ShouldReturnBadRequest_NoFileUploaded()
        {
            IFormFile file = null;
            var result = await _controller.UploadClinicalTrial(file);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("No file uploaded.", badRequestResult.Value);
        }

        [Fact]
        public async Task UploadFile_ShouldReturnBadRequest_InvalidJsonFile()
        {
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(f => f.FileName).Returns("file.json");
            fileMock.Setup(f => f.ContentType).Returns("application/json");
            fileMock.Setup(f => f.Length).Returns(100000);

            var result = await _controller.UploadClinicalTrial(fileMock.Object);
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("The uploaded file is not valid.", badRequestResult.Value);
        }
    }
}