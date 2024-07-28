using GBMProject.Application.Commands.Delivery;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Queries;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    /// Lista todas as entregas cadastradas.
    /// </summary>
    /// <returns>Uma resposta HTTP que indica sucesso na requisição</returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="200">Indica sucesso na listagem das entregas.</response>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Entregas listadas com sucesso.")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllDeliveriesQuery(), cancellationToken);
            return Ok(result);
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
    /// Lista uma entrega correspondente ao Id passado na URL.
    /// </summary>
    /// <param name="id">Id da entrega que será listada.</param>>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="OkObjectResult"/> com a entrega e status 200 (OK) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se a entrega não for encontrada.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="404">Indica que a Entrega não foi encontrada.</response>
    /// <response code="200">Indica sucesso na listagem das entregas.</response>
    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Entregas listadas com sucesso.")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetDeliveryByIdQuery(id), cancellationToken);
            return result.StatusCode switch
            {
                StatusCodes.Status404NotFound => NotFound(result),
                _ => Ok(result)
            };
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
    /// Cria uma nova entrega com os dados fornecidos.
    /// </summary>
    /// <param name="createDeliveryInputDto">Objeto que contém os dados necessários para cadastrar uma nova entrega.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>Uma resposta HTTP que indica o resultado da operação de criação.</returns>
    /// <response code="400">Indica erros nos dados passados pelo usuário.</response>
    /// <response code="404">Indica que a entrega não foi encontrada.</response>
    /// <response code="409">Indica que foi tentado adicionar uma entrega já existente.</response>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
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
    /// Cria uma nova entrega com os dados fornecidos.
    /// </summary>
    /// <param name="id">Id da entrega que terá seu status alterado.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="NoContent"/> com status 204 (No Content) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se a entrega não for encontrada.
    /// Retorna um <see cref="Conflict"/> com status 409 (No Content) se houver conflito.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="409">Indica que houve um conflito.</response>
    /// <response code="404">Indica que a entrega não foi encontrada.</response>
    /// <response code="204">Indica sucesso sem retorno.</response>
    [HttpPut("{id:guid}/in-progress")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro de requisição.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao buscar entrega.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Entrega alterada com sucesso.")]
    public async Task<IActionResult> InProgress(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeliveryStatusInProgressDeliveryCommand(id), cancellationToken);

        return result.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status409Conflict => Conflict(result),
            _ => NoContent()
        };
    }
    
    /// <summary>
    /// Cria uma nova entrega com os dados fornecidos.
    /// </summary>
    /// <param name="id">Id da entrega que terá seu status alterado.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="NoContent"/> com status 204 (No Content) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se a entrega não for encontrada.
    /// Retorna um <see cref="Conflict"/> com status 409 (No Content) se houver conflito.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="409">Indica que houve um conflito.</response>
    /// <response code="404">Indica que a entrega não foi encontrada.</response>
    /// <response code="204">Indica sucesso sem retorno.</response>
    [HttpPut("{id:guid}/finish")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro de requisição.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao buscar entrega.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Entrega alterada com sucesso.")]
    public async Task<IActionResult> Finish(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeliveryStatusFinishedCommand(id), cancellationToken);

        return result.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status409Conflict => Conflict(result),
            _ => NoContent()
        };
    }
    
    /// <summary>
    /// Cria uma nova entrega com os dados fornecidos.
    /// </summary>
    /// <param name="id">Id da entrega que terá seu status alterado.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="NoContent"/> com status 204 (No Content) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se a entrega não for encontrada.
    /// Retorna um <see cref="Conflict"/> com status 409 (No Content) se houver conflito.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="409">Indica que houve um conflito.</response>
    /// <response code="404">Indica que a entrega não foi encontrada.</response>
    /// <response code="204">Indica sucesso sem retorno.</response>
    [HttpPut("{id:guid}/cancel")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Erro de requisição.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao buscar entrega.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada.")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Entrega alterada com sucesso.")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeliveryStatusCanceledCommand(id), cancellationToken);

        return result.StatusCode switch
        {
            StatusCodes.Status404NotFound => NotFound(result),
            StatusCodes.Status409Conflict => Conflict(result),
            _ => NoContent()
        };
    }
}