namespace GBMProject.Application.DTOs.Output;

public class DriverDetailsOutputDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Cpf { get; set; }
    public string CnhCategory { get; set; }
    public DateTime BirthDate { get; set; }
    public string CellPhone { get; set; }
}