using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GBMProject.API.Controllers;

/// <summary>
/// Controller base para todas as outras controllers.
/// </summary>
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Variável readonly que recebe a dependência do mediator pelo construtor.
    /// </summary>
    protected readonly IMediator _mediator;
    
    /// <summary>
    /// Construtor que recebe a dependência injetada do mediator.
    /// </summary>
    /// <param name="mediator">Injeção de dependência passada pelo parâmetro do construtor.</param>
    protected BaseController(IMediator mediator)
        => _mediator = mediator;
}