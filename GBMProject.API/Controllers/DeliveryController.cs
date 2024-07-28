using GBMProject.Application.Commands.Delivery;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GBMProject.API.Controllers;

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
    /// <response code="400">Indica erros por parte do usuário.</response>
    /// <response code="404">Indica que um recurso não foi encontrado.</response>
    /// <response code="409">Indica que foi tentado adicionar um recurso que já existe.</response>
    /// <response code="201">Indica sucesso na criação da entrega.</response>
    
    [HttpPost]
    [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status201Created, "Entrega criada com sucesso", typeof(Result))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de requisição", typeof(Result))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Recurso não encontrado", typeof(Result))]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao criar o recurso", typeof(Result))]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryInputDTO createDeliveryInputDto, CancellationToken cancellationToken)
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
        await _mediator.Send(new InProgressDeliveryCommand { DeliveryId = id }, cancellationToken);

        return Ok();
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