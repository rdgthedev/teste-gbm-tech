using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class CreateTruckInputDTO
{
    [JsonProperty]
    public string Plate { get; set; }= string.Empty;
    
    [JsonProperty]
    public string Model { get; set; }= string.Empty;

    [JsonProperty] public string Color { get; set; } = string.Empty;
    
    [JsonProperty]
    public int NumberOfAxles { get;  set; } 
    [JsonProperty]
    public int YearOfManifacture { get;  set; }
}