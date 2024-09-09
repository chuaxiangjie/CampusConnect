using CampusConnect.Application;
using CampusConnect.Application.Features.Institution;
using CampusConnect.Application.Shared.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CampusConnect.Api.Controllers;

[ApiController]
[Route("api/universities")]
public class UniversityController(ISender mediatr, ILogger<UniversityController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromHeader(Name = "userid")] int userId, [FromQuery] GetListUniversityInput input)
    {
        var result = await mediatr.Send(new GetListUniversityQuery
        {
            Name = input.Name,
            CountryCode = input.CountryCode,
            IsActive = input.IsActive,
            IsBookmark = input.IsBookmark,
            UserId = userId
        });

        return HandleResponse(result, false);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromHeader(Name = "userid")] int userId, [FromRoute] Guid id)
    {
        var result = await mediatr.Send(new GetUniversityQuery
        {
            Id = id,
            UserId = userId
        });

        return HandleResponse(result, false);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUniversityInput input)
    {
        var result = await mediatr.Send(new CreateUniversityCommand
        {
            Name = input.Name,
            CountryId = input.CountryId,
            IsActive = input.IsActive,
            Webpages = input.Webpages
        });

        return HandleResponse(result, false);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateUniversityInput input)
    {
        var result = await mediatr.Send(new UpdateUniversityCommand
        {
            Id = id,
            Name = input.Name,
            CountryId = input.CountryId,
            IsActive = input.IsActive,
            Webpages = input.Webpages
        });

        return HandleResponse(result, true);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await mediatr.Send(new DeleteUniversityCommand
        {
            Id = id
        });

        return HandleResponse(result, true);
    }

    [HttpPost("{id}/bookmark")]
    public async Task<IActionResult> BookmarkAsync([FromHeader(Name = "userid")] int userId, [FromRoute] Guid id)
    {
        var result = await mediatr.Send(new BookmarkUniversityCommand
        {
            Id = id,
            UserId = userId
        });

        return HandleResponse(result, true);
    }

    private IActionResult HandleResponse<T>(Envelope<T> envelope, bool returnEmptyDataIfSuccess = false)
    {
        if (envelope.Response == ResponseType.Error)
            logger.LogError("Error occurred: {Error}", envelope.Error);

         return envelope.Response switch
        {
            ResponseType.NotFound => NotFound(),
            ResponseType.Error => ValidationProblem(envelope.Error ?? "An error occurred."),
            ResponseType.Success when returnEmptyDataIfSuccess => NoContent(),
            ResponseType.Success => Ok(envelope.Data),
            _ => StatusCode(500, "Internal Server Error")
        };
    }
}