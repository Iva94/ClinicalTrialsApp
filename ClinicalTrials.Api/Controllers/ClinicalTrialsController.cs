using ClinicalTrials.Application.Commands.ClinicalTrials.UploadClinicalTrial;
using ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrial;
using ClinicalTrials.Application.Queries.ClinicalTrials.GetClinicalTrialList;
using ClinicalTrials.Contracts.ClinicalTrials;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace ClinicalTrials.Api.Controllers
{
    /// <summary>
    /// Represents the controller for managing the clinical trials.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicalTrialsController : ControllerBase
    {
        private readonly ILogger<ClinicalTrialsController> _logger;
        private readonly IMediator _mediator;
        private readonly JSchema _clinicalTrialSchema;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClinicalTrialsController"/> class.
        /// </summary>
        /// <param name="logger">The logger instance.</param>
        /// <param name="mediator">The mediator instance.</param>
        /// <param name="configuration">The configuration instance.</param>
        public ClinicalTrialsController(ILogger<ClinicalTrialsController> logger, IMediator mediator, IConfiguration configuration)
        {
            _logger = logger;
            _mediator = mediator;
            _clinicalTrialSchema = GetClinicalTrialSchema();
        }

        /// <summary>
        /// Gets the clinical trial with the specified identifier, if it exists.
        /// </summary>
        /// <param name="clinicalTrialId">The clinical trial identifier.</param>
        /// <returns>The clinical trial with the specified identifier, if it exists.</returns>
        [HttpGet("{clinicalTrialId}")]
        [ProducesResponseType(typeof(ClinicalTrialResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClinicalTrial(string clinicalTrialId)
        {
            ArgumentNullException.ThrowIfNull(clinicalTrialId);

            try
            {
                var query = new GetClinicalTrialQuery(clinicalTrialId);
                var clinicalTrial = await _mediator.Send(query);

                if(clinicalTrial == null)
                {
                    return NotFound();
                }

                return Ok(clinicalTrial);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }           
        }

        /// <summary>
        /// Gets the list of clinical trials filtered by title or status.
        /// </summary>
        /// <param name="query">The query parameters.</param>
        /// <returns>Returns list of clinical trials filtered by title or status.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClinicalTrialResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetClinicalTrials([FromQuery] GetClinicalTrialListQuery query)
        {
            ArgumentNullException.ThrowIfNull(query);

            try
            {
                var clinicalTrials = await _mediator.Send(query);
                return Ok(clinicalTrials);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }           
        }

        /// <summary>
        /// Uploads the clinical trial.
        /// </summary>
        /// <param name="clinicalTrialFile">The clinical trial file for upload.</param>
        /// <returns>A 200 (OK) if the upload was successful, otherwise a 400 (Bad Request).</returns>
        [HttpPost("upload")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadClinicalTrial(IFormFile clinicalTrialFile)
        {
            if (clinicalTrialFile == null || clinicalTrialFile.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            if (!clinicalTrialFile.ContentType.Equals("application/json", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Only JSON files are allowed.");
            }

            try
            {
                using var reader = new StreamReader(clinicalTrialFile.OpenReadStream());
                var jsonContent = await reader.ReadToEndAsync();

                if (!ValidateJsonSchema(jsonContent, out IList<string> errors))
                {
                    
                    string errorMessage = "Invalid JSON format according to the schema. ";
                    if (errors.Any())
                    {
                        errorMessage += string.Join(", \n", errors);
                    }
                    
                    return BadRequest(errorMessage);
                }

                var clinicalTrialMetadata = JsonConvert.DeserializeObject<ClinicalTrialMetadata>(jsonContent, 
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented
                    });

                if (clinicalTrialMetadata == null || string.IsNullOrEmpty(clinicalTrialMetadata.TrialId))
                {
                    _logger.Log(LogLevel.Error, "Cannot deserialize object. JSON content: " + jsonContent);
                    return BadRequest("JSON file cannot be deserialized");
                }

                UploadClinicalTrialCommand uploadClinicalTrialCommand = new UploadClinicalTrialCommand()
                {
                    ClinicalTrialId = clinicalTrialMetadata.TrialId,
                    Title = clinicalTrialMetadata.Title,
                    StartDate = clinicalTrialMetadata.StartDate,
                    EndDate = clinicalTrialMetadata.EndDate ?? clinicalTrialMetadata.StartDate.AddDays(30),
                    Participants = clinicalTrialMetadata.Participants,
                    Status = clinicalTrialMetadata.Status,
                };

                var clinicalTrialId = await _mediator.Send(uploadClinicalTrialCommand);

                return Ok(new { Message = "File uploaded successfully.", TrialId = clinicalTrialId });
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Gets the clinical trial JSON schema from embeded resource.
        /// </summary>
        /// <returns>The clinical trial JSON schema.</returns>
        private JSchema GetClinicalTrialSchema()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resource = assembly.GetManifestResourceNames().Where(x => x.Contains("ClinicalTrialSchema.json")).First();
            var resourceStream = assembly.GetManifestResourceStream(resource);
            using var reader = new StreamReader(resourceStream);
            var fileContent = reader.ReadToEnd();
            return JSchema.Parse(fileContent);
        }

        /// <summary>
        /// Validates uploaded JSON file against provided JSON schema.
        /// </summary>
        /// <param name="jsonContent">Uploaded JSON file content.</param>
        /// <param name="errors">List of error that can occur during validation.</param>
        /// <returns>True if uploaded JSON file is valid, or false if uploaded JSON file is not valid.</returns>
        private bool ValidateJsonSchema(string jsonContent, out IList<string> errors)
        {
            var json = JObject.Parse(jsonContent);
            return json.IsValid(_clinicalTrialSchema, out errors);
        }
    }
}
