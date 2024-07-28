namespace GBMProject.Application.Commands.Validations.Common;

public static class ValidateMinimumValueDate
{
    public static bool IsValid(DateTime? birthDate)
        => !birthDate!.Value.Equals(default);
}