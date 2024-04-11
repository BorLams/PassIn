namespace PassIn.Application.Data.Dtos.Error.Response;
public class ResponseErrorsDto
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorsDto(IEnumerable<string> errorMessages)
    {
        ErrorMessages.AddRange(errorMessages);
    }
}
