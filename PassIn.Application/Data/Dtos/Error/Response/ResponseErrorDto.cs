namespace PassIn.Application.Data.Dtos.Error.Response;
public class ResponseErrorDto
{
    public string ErrorMessage { get; set; }

    public ResponseErrorDto(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
