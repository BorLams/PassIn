namespace PassIn.Application.Validations;

public class ValidationResult
{
    public List<string> ErrorMessages { get; }
    public bool IsValid => ErrorMessages.Count == 0;

    private readonly string _errorMessagesException = "All error messages must have a value.";
    private readonly string _errorMessageException = "Error message must have a value.";

    #region Constructors
    public ValidationResult()
    {
        ErrorMessages = [];
    }

    public ValidationResult(string errorMessage)
    {
        ErrorMessages = [errorMessage];
    }

    public ValidationResult(IEnumerable<string> errorMessages)
    {
        ErrorMessages = errorMessages.ToList();
    }
    #endregion

    public void AddError(string errorMessage)
    {
        if (ValidateErrorMessage(errorMessage))
        {
            throw new ApplicationException(_errorMessageException);
        }
        else
        {
            ErrorMessages.Add(errorMessage);
        }
    }

    public void AddError(IEnumerable<string> errorMessages)
    {
        if (ValidateErrorMessage(errorMessages))
            ErrorMessages.AddRange(errorMessages);
        else
            throw new ApplicationException(_errorMessagesException);
    }

    public void AddValidationResult(ValidationResult validation)
    {
        if (!validation.IsValid)
            ErrorMessages.AddRange(validation.ErrorMessages);
    }

    #region Private Methods
    private static bool ValidateErrorMessage(IEnumerable<string> errorMessages)
        => errorMessages.Any() && !errorMessages.Any(string.IsNullOrWhiteSpace);

    private static bool ValidateErrorMessage(string errorMessage)
        => string.IsNullOrWhiteSpace(errorMessage);
    #endregion
}