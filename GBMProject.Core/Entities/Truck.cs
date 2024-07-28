using GBMProject.Core.Common;

namespace GBMProject.Core.Entities;

public class Truck : Entity
{
    public Truck(
        string plate,
        string model,
        string color,
        int yearOfManifacture,
        int numberOfAxles)
    {
        Plate = plate;
        Model = model;
        Color = color;
        NumberOfAxles = numberOfAxles;
        YearOfManifacture = yearOfManifacture;
    }

    public string Plate { get; private set; }
    public string Model { get; private set; }
    public int YearOfManifacture { get; private set; }
    public string Color { get; private set; }
    public int NumberOfAxles { get; private set; }
    public IReadOnlyCollection<Delivery> Deliveries { get; private set; }

    public void ChangePlate(string plate)
    {
        if (string.IsNullOrEmpty(plate) || Plate.Equals(plate))
            return;

        Plate = plate;
    }

    public void ChangeColor(string model)
    {
        if (string.IsNullOrEmpty(model) || Model.Equals(model))
            return;

        Model = model;
    }
}