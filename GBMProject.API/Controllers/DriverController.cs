using GBMProject.Application.Commands.Driver;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Queries;
using GBMProject.Application.Queries.Driver;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace GBMProject.API.Controllers;

[ApiController]
[Route("v1/api/drivers")]
public class DriverController : BaseController
{
    public DriverController(IMediator mediator) : base(mediator)
    {
    }

    /// <summary>
    /// Lista todos os motoristas cadastradados.
    /// </summary>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="OkObjectResult"/> com a entrega e status 200 (OK) se a operação for bem-sucedida.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
    /// <response code="200">Indica sucesso na listagem dos motoristas.</response>
    [HttpGet]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Entregas listadas com sucesso.")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllDriversQuery(), cancellationToken);
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
    /// Lista um motorista específico de acordo com o Id passado na URL
    /// </summary>
    /// <param name="id">Id do motorista que será listado.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>
    /// Um <see cref="IActionResult"/> que representa a resposta HTTP.
    /// Retorna um <see cref="OkObjectResult"/> com a entrega e status 200 (OK) se a operação for bem-sucedida.
    /// Retorna um <see cref="NotFoundResult"/> com status 404 (Not Found) se o motorista não for encontrado.
    /// Retorna um <see cref="ObjectResult"/> com status 500 (Internal Server Error) se ocorrer um erro interno.
    /// </returns>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
    /// <response code="404">Indica que o motorista não foi encontrado.</response>
    /// <response code="200">Indica sucesso na listagem do motorista.</response>
    [HttpGet("{id:guid}")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Ocorreu um erro interno.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Motorista não encontrado.")]
    [SwaggerResponse(StatusCodes.Status200OK, "Motorista listado com sucesso.")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetDriverByIdQuery(id), cancellationToken);
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
    /// Cadastra um novo motorista com os dados fornecidos.
    /// </summary>
    /// <param name="dto">Objeto que contém os dados necessários para cadastrar um novo motorista.</param>
    /// <param name="cancellationToken">Token que monitora e recebe solicitações de cancelamento.</param>
    /// <returns>Uma resposta HTTP que indica o resultado da operação de criação.</returns>
    /// <response code="400">Indica erros nos dados passados pelo usuário.</response>
    /// <response code="404">Indica que o motorista não foi encontrado.</response>
    /// <response code="409">Indica que foi tentado adicionar um motorista já cadastrado.</response>
    /// <response code="500">Indica que ocorreu um erro interno.</response>
    /// <response code="201">Indica sucesso no cadastro do motorista.</response>
    [HttpPost]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status201Created, "Entrega criada com sucesso")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Erro de requisição")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Entrega não encontrada")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "Conflito ao criar o recurso")]
    public async Task<IActionResult> Create([FromBody] CreateDriverInputDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateDriverCommand(dto.Name, dto.Cpf, dto.CnhCategory, dto.BirthDate, dto.Phone);
            var result = await _mediator.Send(command, cancellationToken);

            return result.StatusCode switch
            {
                StatusCodes.Status400BadRequest => BadRequest(result),
                StatusCodes.Status409Conflict => Conflict(result),
                _ => Created($"v1/api/drivers/{result.Data}", result)
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

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateDriverInputDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new UpdateDriverCommand(id, dto.Name, dto.CnhCategory!, dto.Phone!);
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