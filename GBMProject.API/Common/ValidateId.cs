namespace GBMProject.Application.Commands.Validations.Common;

public static class ValidateId
{
    public static bool IsValid(Guid? guid)
        => guid != Guid.Empty;
}