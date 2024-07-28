using GBMProject.Core.Common;
using GBMProject.Core.Enums;

namespace GBMProject.Core.Entities;

public class Driver : Entity
{
    public Driver(
        string name,
        string cpf,
        ECnhCategory cnhCategory,
        DateTime birthDate,
        string phone)
    {
        Name = name;
        Cpf = cpf;
        CnhCategory = cnhCategory;
        BirthDate = birthDate;
        Phone = phone;
    }

    public string Name { get; private set; }
    public string Cpf { get; private set; }
    public ECnhCategory CnhCategory { get; private set; }
    public DateTime BirthDate { get; private set; }
    public string Phone { get; private set; }
    public IReadOnlyCollection<Delivery> Deliveries { get; private set; }

    public void ChangeName(string name)
    {
        if (string.IsNullOrEmpty(name))
            return;
        
        Name = name;
    }

    public void ChangeCnhCategory(string cnhCategory)
    {
        var isEqual = CnhCategory.ToString().Equals(cnhCategory, StringComparison.OrdinalIgnoreCase);
        
        if (isEqual)
            return;

        if(Enum.TryParse(typeof(ECnhCategory), cnhCategory, ignoreCase: true, out var category))
            CnhCategory = (ECnhCategory)category;
    }

    public void ChangePhone(string phone)
    {
        if (string.IsNullOrEmpty(phone))
            return;
        
        Phone = phone;
    }
}