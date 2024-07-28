using GBMProject.Core.Enums;

namespace GBMProject.Application.Commands.Validations.Common;

public static class ValidateCategoryType
{
    public static bool IsValid(string category)
        => category switch
        {
            _ when string.Equals(category, nameof(ECnhCategory.C), StringComparison.OrdinalIgnoreCase) => true,
            _ when string.Equals(category, nameof(ECnhCategory.D), StringComparison.OrdinalIgnoreCase) => true,
            _ when string.Equals(category, nameof(ECnhCategory.E), StringComparison.OrdinalIgnoreCase) => true,
            _ => false
        };
}