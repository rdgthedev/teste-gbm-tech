using GBMProject.Application.Queries.Common;

namespace GBMProject.Application.Queries.Driver;

public class GetDriverByIdQuery : BaseQuery
{
    public GetDriverByIdQuery(Guid driverId)
        => DriverId = driverId;

    public Guid DriverId { get; private set; }
}