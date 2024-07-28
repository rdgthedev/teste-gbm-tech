using GBMProject.Application.Results;
using MediatR;

namespace GBMProject.Application.Commands.Common.Abstractions;

public abstract class Command : IRequest<Result>
{
    
}