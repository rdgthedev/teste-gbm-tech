using GBMProject.Application.Queries.Common;

namespace GBMProject.Application.Queries.Truck;

public class GetTruckByIdQuery : BaseQuery
{
    public GetTruckByIdQuery(Guid truckId)
        => TruckId = truckId;
    
    public Guid TruckId { get; private set; }
}