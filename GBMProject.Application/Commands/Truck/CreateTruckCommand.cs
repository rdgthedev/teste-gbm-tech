using GBMProject.Application.Commands.Common;
using GBMProject.Application.Commands.Common.Abstracts;

namespace GBMProject.Application.Commands.Truck;

public class CreateTruckCommand : Command
{
    public CreateTruckCommand(
        string plate, 
        string model,
        string color,
        int numberOfAxles)
    {
        Plate = plate;
        Model = model;
        Color = color;
        NumberOfAxles = numberOfAxles;
    }

    public string Plate { get; private set; } 
    public string Model { get;  private set; } 
    public string Color { get;  private set; } 
    public int NumberOfAxles { get;  private set; }
}