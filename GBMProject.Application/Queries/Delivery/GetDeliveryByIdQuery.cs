﻿using GBMProject.Application.Queries.Common;

namespace GBMProject.Application.Queries.Delivery;

public class GetDeliveryByIdQuery : BaseQuery
{
    public GetDeliveryByIdQuery(Guid id)
        => Id = id;

    public Guid Id { get; private set; }
}