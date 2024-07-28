using GBMProject.Application.Results;
using MediatR;

namespace GBMProject.Application.Commands.Common.Abstracts;

public abstract class Command : IRequest<Result>
{
    
}