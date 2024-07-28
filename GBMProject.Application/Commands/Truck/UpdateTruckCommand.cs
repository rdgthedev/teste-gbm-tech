using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Truck;

public class UpdateTruckCommand : Command
{
    public UpdateTruckCommand(
        Guid id, 
        string plate,
        string color)
    {
        Id = id;
        Plate = plate;
        Color = color;
    }

    public Guid Id { get; private set; }
    public string Plate { get; private set; } 
    public string Color { get; private set; }
}