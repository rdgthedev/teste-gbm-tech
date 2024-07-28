
using System.ComponentModel.DataAnnotations;
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
    [DataType(DataType.Date)]
    public DateTime? BirthDate { get; set; }
    [JsonProperty]
    public string CellPhone { get; set; } = string.Empty;
}