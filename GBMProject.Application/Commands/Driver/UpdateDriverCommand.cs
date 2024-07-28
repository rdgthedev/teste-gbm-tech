using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Driver;

public class UpdateDriverCommand : Command
{
    public UpdateDriverCommand(
        Guid? driverId,
        string name,
        string cnhCategory,
        string phone)
    {
        DriverId = driverId ??= Guid.Empty;
        Name = name;
        CnhCategory = cnhCategory;
        Phone = phone;
    }
    
    public Guid? DriverId { get; private set; }
    public string Name { get; private set; } 
    public string CnhCategory { get; private set; }
    public string Phone { get; private set; }
}