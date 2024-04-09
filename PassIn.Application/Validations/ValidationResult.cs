namespace PassIn.Application.Validations;

public class ValidationResult
{
    public List<string> ErrorMessages { get; }
    public bool IsValid => ErrorMessages.Count == 0;

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

    public void AddValidation(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
        {
            throw new ApplicationException("Error message cannot be null.");
        }
        else
        {
            ErrorMessages.Add(errorMessage);
        }
    }

    public void AddValidation(IEnumerable<string> errorMessages)
    {
        if (errorMessages.Any() && !errorMessages.Any(string.IsNullOrWhiteSpace))
        {
            ErrorMessages.AddRange(errorMessages);
        }
        else
        {
            throw new ApplicationException("All error messages must have a value.");
        }
    }
}