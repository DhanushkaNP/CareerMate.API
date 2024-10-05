using CareerMate.Abstractions;
using CareerMate.Abstractions.Enums;
using CareerMate.API.Controllers;
using CareerMate.EndPoints.Commands.Batches.Create;
using CareerMate.EndPoints.Commands.Batches.Update;
using CareerMate.EndPoints.Handlers;
using CareerMate.EndPoints.Queries.Batches.GetBatchesSuggestionList;
using CareerMate.EndPoints.Queries.Batches.GetDetails;
using CareerMate.EndPoints.Queries.Batches.GetListByFaculty;
using CareerMate.EndPoints.Queries.Universities.GetSuggestionsList;
using CsvHelper;
using CsvHelper.Configuration;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CareerMate.Controllers
{
    [Route("api/Faculty/{facultyId:Guid}/StudentBatches")]
    [ApiController]
    [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
    public class StudentBatchController : BaseController
    {
        private readonly IMediator _mediator;

        public StudentBatchController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> CreateBatch([FromRoute] Guid facultyId, [FromForm] CreateBatchCommand command, [Required] IFormFile studentCsv, CancellationToken cancellationToken)
        {
            command.FacultyId = facultyId;

            using (var memoryStream = new MemoryStream())
            {
                await studentCsv.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                using (var reader = new StreamReader(memoryStream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
                {
                    csv.Read();
                    csv.ReadHeader();
                    var headers = csv.HeaderRecord;

                    if (!headers.Contains("StudentId") || !headers.Contains("Email"))
                    {
                        return ToActionResult(new BadRequestResponse(ErrorCodes.StudentCsvNotInCorrectFormat, "Incorrect CSV format"));
                    }

                    var students = csv.GetRecords<StudentCsvModel>().ToList();
                    command.Students = students;
                }
            }

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Policies.CoordinatorAssistantLevel)]
        public async Task<IActionResult> GetFacultyStudentBatches([FromRoute] Guid facultyId, [FromQuery] FacultyStudentBatchesListQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorLevel)]
        public async Task<IActionResult> GetFacultyStudentBatchDetails([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetFacultyStudentBatchDetailsQuery { Id = id };

            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }

        [HttpPut("{id:Guid}")]
        [Authorize(Policy = Policies.CoordinatorOnly)]
        public async Task<IActionResult> UpdateFacultyStudentBatch([FromRoute] Guid id, [FromBody] UpdateFacultyStudentBatchCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            return ToActionResult(result);
        }

        [HttpGet("Suggestions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBatchesSuggestions([FromRoute]Guid facultyId, [FromQuery] GetBatchSuggestionsQuery query, CancellationToken cancellationToken)
        {
            query.FacultyId = facultyId;
            var result = await _mediator.Send(query, cancellationToken);
            return ToActionResult(result);
        }
    }
}
