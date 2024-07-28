using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class CreateDeliveryInputDTO
{
    [JsonProperty]
    public DateTime? DeliveryDate { get; set; }
    [JsonProperty]
    public string Origin { get; set; } = string.Empty!;
    [JsonProperty]
    public string Destiny { get; set; } = string.Empty!;
    [JsonProperty]
    public string CargoTransported { get; set; } = string.Empty!;
    [JsonProperty]
    public Guid? TruckId { get; set; }
    [JsonProperty]
    public Guid? DriverId { get; set; }
}