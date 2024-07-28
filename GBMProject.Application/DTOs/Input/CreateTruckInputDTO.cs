using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class CreateTruckInputDTO
{
    [JsonProperty]
    public string Plate { get; set; }
    
    [JsonProperty]
    public string Model { get; set; }
    [JsonProperty]
    public string Color { get; set; }
    
    [JsonProperty]
    public int NumberOfAxles { get;  set; } 
}