using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class UpdateTruckInputDTO
{
    [JsonProperty]
    public string Plate { get; set; } = string.Empty;
    [JsonProperty]
    public string Color { get; set; } = string.Empty;
}