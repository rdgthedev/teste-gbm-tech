using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class UpdateDriverInputDTO
{
    [JsonProperty] 
    public string Name { get; set; } = string.Empty;
    [JsonProperty] 
    public string? CnhCategory { get; set; } = string.Empty;
    [JsonProperty] 
    public string? Phone { get; set; } = string.Empty;
}