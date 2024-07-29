namespace GBMProject.Application.DTOs.Output;

public class TruckDetailsOutputDTO
{
    public Guid Id { get; set; }
    public string Plate { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int YearOfManifacture { get; set; }
    public string Color { get; set; } = null!;
    public int NumberOfAxles { get; set; }
}