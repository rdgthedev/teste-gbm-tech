namespace GBMProject.Application.DTOs.Output;

public class DeliveryDetailsOutputDTO
{
    public Guid Id { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string Origin { get; set; } = null!;
    public string Destiny { get; set; } = null!;
    public string CargoTransported { get; set; } = null!;
    public string DeliveryStatus { get; set; } = null!;
    public DriverDetailsOutputDTO DriverDetails { get; set; } = null!;
    public TruckDetailsOutputDTO TruckDetails { get; set; } = null!;
}