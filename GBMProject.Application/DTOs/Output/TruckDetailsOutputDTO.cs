namespace GBMProject.Application.DTOs.Output;

public class TruckDetailsOutputDTO
{
    public Guid Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public DateTime YearOfManifacture { get; set; }
    public string Color { get; set; } = string.Empty;
    public int NumberOfAxles { get; set; }
}