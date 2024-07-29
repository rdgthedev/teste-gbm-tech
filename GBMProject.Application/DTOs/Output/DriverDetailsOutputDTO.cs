namespace GBMProject.Application.DTOs.Output;

public class DriverDetailsOutputDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Cpf { get; set; } = null!;
    public string CnhCategory { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string CellPhone { get; set; } = null!;
}