using Microsoft.AspNetCore.Mvc;
using PassIn.Application.Data.Dtos.CheckIn.Request;
using PassIn.Application.Data.Dtos.CheckIn.Response;
using PassIn.Application.Data.Dtos.Error.Response;
using PassIn.Application.Services.Interfaces;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    #region Dependency Injections
    private readonly ICheckInsService _service;
    public CheckInController(ICheckInsService service)
    {
        _service = service;
    }
    #endregion

    [HttpGet]
    [ProducesResponseType<ResponseCheckInDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseErrorsDto>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _service.GetAllAsync();

        return response.Any() ? Ok(response) : NotFound(new ResponseErrorDto("No CheckIn's found."));
    }

    [HttpPost]
    [ProducesResponseType<ResponseCheckInDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseErrorsDto>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckInAsync([FromBody] RequestCheckInDto request)
    {
        (var validation, var response) = await _service.CheckInAsync(request);

        return validation.IsValid ? Created(string.Empty, response) : BadRequest(new ResponseErrorsDto(validation.ErrorMessages));
    }
}