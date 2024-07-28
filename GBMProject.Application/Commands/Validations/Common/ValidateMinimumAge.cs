namespace GBMProject.Application.Commands.Validations.Common;

public static class ValidateMinimumAge
{
    public static bool IsValid(DateTime? birthDate, int years)
        => birthDate!.Value.Date <= DateTime.Now.Date.AddYears(-21);
}