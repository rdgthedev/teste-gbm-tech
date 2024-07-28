using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Output;

public class TruckDetailsOutputDTO
{
    public Guid Id { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public int YearOfManifacture { get; set; }
    public string Color { get; set; }
    public int NumberOfAxles { get; set; }
}