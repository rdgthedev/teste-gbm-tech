namespace GBMProject.Application.DTOs.Output;

public class DeliveryDetailsOutputDTO
{
    public Guid Id { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destiny { get; set; } = string.Empty;
    public string CargoTransported { get; set; } = string.Empty;
    public string DeliveryStatus { get; set; } = string.Empty;
    public TruckDetailsOutputDTO TruckDetails { get; set; } = null!;
    public DriverDetailsOutputDTO DriverDetails { get; set; } = null!;
}