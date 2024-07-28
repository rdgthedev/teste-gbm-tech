using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstractions;

namespace GBMProject.Application.Commands.Driver;

public class CreateDriverCommand : Command
{
    public CreateDriverCommand(
        string name,
        string cpf,
        string cnhCategory,
        DateTime? birthDate,
        string phone)
    {
        Name = name;
        Cpf = cpf;
        CnhCategory = cnhCategory;
        BirthDate = birthDate ??= DateTime.MinValue;
        Phone = phone;
    }

    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public string CnhCategory { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public string Phone { get; private set; }
}