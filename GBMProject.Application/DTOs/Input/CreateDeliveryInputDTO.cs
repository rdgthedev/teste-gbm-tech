using Newtonsoft.Json;

namespace GBMProject.Application.DTOs.Input;

public class CreateDeliveryInputDTO
{
    /// <summary>
    /// Data da entrega
    /// </summary>
    [JsonProperty]
    public DateTime? DeliveryDate { get; set; }
    
    /// <summary>
    /// Origem da entrega
    /// </summary>
    [JsonProperty]
    public string Origin { get; set; } = string.Empty;
    
    /// <summary>
    /// Destino da entrega
    /// </summary>
    [JsonProperty]
    public string Destiny { get; set; } = string.Empty;
    
    /// <summary>
    /// Carga transportada
    /// </summary>
    [JsonProperty]
    public string CargoTransported { get; set; } = string.Empty;
    
    /// <summary>
    /// Id do caminhão
    /// </summary>
    [JsonProperty]
    public Guid? TruckId { get; set; }
    
    /// <summary>
    /// Id do motorista
    /// </summary>
    [JsonProperty]
    public Guid? DriverId { get; set; }
}