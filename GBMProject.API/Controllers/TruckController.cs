using GBMProject.Application.Commands.Truck;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Queries.Truck;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GBMProject.API.Controllers;

/// <summary>
/// Controller responsável por gerenciar os endpoints de caminhões.
/// </summary>
[ApiController]
[Route("v1/api/trucks")]
public class TruckController : BaseController
{
    /// <summary>
    /// Construtor responsável por receber as dependências
    /// </summary>
    /// <param name="mediator">Mediator é a injetado como dependência da controller</param>
    public TruckController(IMediator mediator) : base(mediator)
    {
    }
    
    /// <summary>
    /// Lista todos os caminhões cadastrados.
    /// </summary>
    /// <returns>Uma resposta HTTP que indica sucesso na operação de listagem.</returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="200">Indica sucesso na listagem dos caminhões.</response>
    [HttpGet]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllTrucksQuery(), cancellationToken);
            
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
    /// Lista um caminhão específico de acordo com o Id passado na URL.
    /// </summary>
    /// <param name="id">Id do caminhão que será listado.</param>>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="OkObjectResult"/> com a entrega e status 200 (OK) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se a entrega não for encontrada.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorre um erro interno.</response>
    /// <response code="404">Indica que o caminhão não foi encontrada.</response>
    /// <response code="200">Indica sucesso na listagem do caminhão.</response>
    [HttpGet("{id:Guid}")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caminhão não encontrado.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Caminhão listado com sucesso.")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetTruckByIdQuery(id), cancellationToken);
            
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
    /// Cadastra um novo caminhão com os dados fornecidos.
    /// </summary>
    /// <param name="dto">Objeto que contém os dados necessários para cadastrar um novo caminhão.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>Uma resposta HTTP que indica o resultado da operação de criação.</returns>
    /// <response code="400">Indica erros nos dados passados pelo usuário.</response>
    /// <response code="409">Indica que foi tentado cadastrar cadastrar um caminhão que já está cadastrado.</response>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
    /// <response code="201">Indica sucesso no cadastro do caminhão.</response>
    [HttpPost]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno")]
    [SwaggerResponse(StatusCodes.Status201Created, "Caminhão cadastrada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros referente aos dados passado no corpo da requisição.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Caminhão já cadastrado.")]
    public async Task<IActionResult> Create([FromBody] CreateTruckInputDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateTruckCommand(dto.Plate, dto.Model, dto.Color, dto.YearOfManifacture, dto.NumberOfAxles);
            
            var result = await _mediator.Send(command, cancellationToken);

            return result.StatusCode switch
            {
                StatusCodes.Status400BadRequest => BadRequest(result),
                StatusCodes.Status409Conflict => Conflict(result),
                _ => Created($"v1/api/trucks/{result.Data}", result)
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
    /// Atualiza os dados de um caminhão.
    /// </summary>
    /// <param name="id">Id da entrega que terá seu status alterado.</param>
    /// <param name="dto">Objeto que contém os dados possíveis de alterar de um caminhão.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="NoContent"/> com status 204 (No Content) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se o caminhão não for encontrada.
    /// Retorna um <see cref="Conflict"/> com status 409 (No Content) se houver conflito.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
    /// <response code="409">Indica que a placa passada já está cadastrada.</response>
    /// <response code="404">Indica que o caminhão não foi encontrada.</response>
    /// <response code="204">Indica sucesso, mas não tem retorno de uma resposta.</response>
    [HttpPut("{id:guid}")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status201Created, "Caminhão cadastrado com sucesso.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erros referente aos dados passado no corpo da requisição.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Caminhão não encontrado.")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Placa já cadastrada.")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateTruckInputDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new UpdateTruckCommand(id, dto.Plate, dto.Color);
            var result = await _mediator.Send(command, cancellationToken);

            return result.StatusCode switch
            {
                StatusCodes.Status400BadRequest => BadRequest(result),
                StatusCodes.Status409Conflict => Conflict(result),
                _ => NoContent()
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
}