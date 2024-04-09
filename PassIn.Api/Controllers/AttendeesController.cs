using Microsoft.AspNetCore.Mvc;
using PassIn.Application.Data.Dtos.Attendee.Request;
using PassIn.Application.Data.Dtos.Attendee.Response;
using PassIn.Application.Data.Dtos.Error.Response;
using PassIn.Application.Services.Interfaces;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    #region Dependency Injections
    private readonly IAttendeesService _service;

    public AttendeesController(IAttendeesService service)
    {
        _service = service;
    }
    #endregion

    [HttpPost]
    [ProducesResponseType<ResponseAttendeeDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseErrorsDto>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestRegisterAttendeeDto request)
    {
        (var validation, var response) = await _service.RegisterAttendeeAsync(request);

        return validation.IsValid ? Created(string.Empty, response) : BadRequest(validation.ErrorMessages);
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<ResponseAttendeeDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var attendees = await _service.GetAllAsync();

        return attendees.Any() ? Ok(attendees) : NotFound(new ResponseErrorDto("There's no attendees registered."));
    }

    [HttpGet]
    [Route("{eventId:guid}")]
    [ProducesResponseType<IEnumerable<ResponseAttendeeDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllByEventIdAsync([FromRoute] Guid eventId)
    {
        var attendeesInEvent = await _service.GetAllByEventIdAsync(eventId);

        return attendeesInEvent.Any() ? Ok(attendeesInEvent) : NotFound(new ResponseErrorDto("There's no attendees registered for this event."));
    }
}
