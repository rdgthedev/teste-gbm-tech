using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class CreateDriverInputDTO
{
    [JsonProperty]
    public string Name { get; set; } = string.Empty;
    [JsonProperty]
    public string Cpf { get; set; } = string.Empty;
    [JsonProperty]
    public string CnhCategory { get; set; } = string.Empty;
    [JsonProperty]
    public DateTime? BirthDate { get; set; }
    [JsonProperty]
    public string Phone { get; set; } = string.Empty;
}