namespace GBMProject.Core.Common;

public class Entity
{
    protected Entity()
        => Id = Guid.NewGuid();

    public Guid Id { get; private set; }
}