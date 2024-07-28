using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Output;

public class DeliveryDetailsOutputDTO
{
    [JsonProperty]
    public Guid Id { get; set; }
    [JsonProperty]
    public DateTime DeliveryDate { get; set; }
    [JsonProperty]
    public string Origin { get; set; } = string.Empty;
    [JsonProperty]
    public string Destiny { get; set; } = string.Empty;
    [JsonProperty]
    public string CargoTransported { get; set; } = string.Empty;
    [JsonProperty]
    public string DeliveryStatus { get; set; } = string.Empty;
}