using GBMProject.Application.Commands.Truck;
using GBMProject.Application.DTOs.Input;
using GBMProject.Application.Queries.Truck;
using GBMProject.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace GBMProject.API.Controllers;

[ApiController]
[Route("v1/api/trucks")]
public class TruckController : BaseController
{
    public TruckController(IMediator mediator) : base(mediator)
    {
    }
    
    [HttpGet]
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTruckInputDTO dto, CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateTruckCommand(dto.Plate, dto.Model, dto.Color, dto.NumberOfAxles);
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

    [HttpPut("{id:guid}")]
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