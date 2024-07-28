using GBMProject.Application.Commands.Delivery;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Swashbuckle.AspNetCore.Annotations;

namespace GBMProject.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar os endpoints de entregas.
/// </summary>
[ApiController]
[Route("v1/api/deliveries")]
public class DeliveryController : BaseController
{
    public DeliveryController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Cria uma nova entrega com os dados fornecidos.
    /// </summary>
    /// <param name="createDeliveryInputDto">Objeto que contém os dados necessários para cadastrar uma nova entrega.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>Uma resposta HTTP que indica o resultado da operação de criação.</returns>
    /// <response code="400">Indica erros nos dados passados pelo usuário.</response>
    /// <response code="404">Indica que a entrega não foi encontrada.</response>
    /// <response code="409">Indica que foi tentado adicionar uma entrega já existente.</response>
    /// <response code="201">Indica sucesso na criação da entrega.</response>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status201Created, "Entrega criada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de requisição")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao criar o recurso")]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryInputDTO createDeliveryInputDto,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateDeliveryCommand(
                createDeliveryInputDto.DeliveryDate,
                createDeliveryInputDto.Origin,
                createDeliveryInputDto.Destiny,
                createDeliveryInputDto.CargoTransported,
                createDeliveryInputDto.TruckId,
                createDeliveryInputDto.DriverId);
            var result = await _mediator.Send(command, cancellationToken);

            return result.StatusCode switch
            {
                StatusCodes.Status400BadRequest => BadRequest(result),
                StatusCodes.Status404NotFound => NotFound(result),
                StatusCodes.Status409Conflict => Conflict(result),
                _ => Created($"v1/api/deliveries/{result.Data}", result)
            };
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Result(StatusCodes.Status500InternalServerError, "Ocorreu um erro na base de dados"));
        }
        catch (SqlException)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Result(StatusCodes.Status500InternalServerError, "Ocorreu um erro na base de dados"));
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Result(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno"));
        }
    }

    /// <summary>
    /// Atualiza o status de uma determinada entrega para "Em execução".
    /// </summary>
    /// <param name="id">Identificador de uma entrega específica</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>Uma resposta HTTP que indica o resultado da operação de criação.</returns>
    /// <response code="404">Indica que um recurso não foi encontrado.</response>
    /// <response code="204">Indica sucesso na criação da entrega.</response>
    [HttpPut("{id:guid}/in-progress")]
    public async Task<IActionResult> InProgress(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new InProgressDeliveryCommand(id), cancellationToken);

        return result.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status409Conflict => Conflict(result),
            _ => NoContent()
        };
    }

    [HttpPut("{id:guid}/finish")]
    public async Task<IActionResult> Finish()
    {
        return Ok();
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel()
    {
        return Ok();
    }
}