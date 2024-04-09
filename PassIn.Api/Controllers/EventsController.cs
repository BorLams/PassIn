using Microsoft.AspNetCore.Mvc;
using PassIn.Application.Data.Dtos.Error.Response;
using PassIn.Application.Data.Dtos.Event.Request;
using PassIn.Application.Data.Dtos.Event.Response;
using PassIn.Application.Services.Interfaces;
using PassIn.Application.Validations;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    #region Dependency Injections
    private readonly IEventsService _service;

    public EventsController(IEventsService service)
    {
        _service = service;
    }
    #endregion

    [HttpPost]
    [ProducesResponseType<ResponseRegisteredEventDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseErrorsDto>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RequestEventDto request)
    {
        var (validation, response) = await _service.RegisterEventAsync(request);

        return validation.IsValid ? Created(string.Empty, response) : BadRequest(validation.ErrorMessages);
    }

    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType<ResponseEventDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _service.GetByIdAsync(id);

        var validation = new ValidationResult();
        if (response is null)
            validation.AddValidation("No event was found.");

        return validation.IsValid ? Ok(response) : NotFound(validation.ErrorMessages.First());
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<ResponseEventDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _service.GetAllAsync();

        var validation = new ValidationResult();
        if (!response.Any())
            validation.AddValidation("There's no event registered.");

        return validation.IsValid ? Ok(response) : NotFound(new ResponseErrorDto(validation.ErrorMessages.First()));
    }
}
