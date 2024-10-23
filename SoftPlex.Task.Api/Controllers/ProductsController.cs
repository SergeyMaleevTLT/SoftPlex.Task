using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoftPlex.Task.Api.Commands;
using SoftPlex.Task.Api.Queries;
using SoftPlex.Task.Core.Domain.Dto;


namespace SoftPlex.Task.API.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class ProductsController : Controller
{
    private readonly IMediator _mediator;
    private readonly IProductsQuery _query;

    public ProductsController(IMediator mediator, IProductsQuery query)
    {
        _mediator = mediator;
        _query = query;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] ProductDto request, CancellationToken token)
    {
        var response = await _mediator.Send(new AddProductCommand { Model = request }, token);
        return CreatedAtAction(nameof(GetById), new {id = response.Id}, response);
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType((StatusCodes.Status404NotFound))]
    public async Task<IActionResult> Put([FromBody] ProductDto request, CancellationToken token)
    {
        var response = await _mediator.Send(request, token);
        return Ok(response);
    }
    
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken token)
    {
        var result = await _mediator.Send(new DeleteProductCommand { ModelId = id }, token);
        return result
            ? Ok()
            : NotFound();
    }

    [HttpGet("GetById/{id:guid}")]
    [ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken token)
    {
        var result = await _query.GetByIdAsync(id, token);
        return result is not null
            ? Ok(result)
            : NotFound();
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult<ProductDto[]>> SearchByName([FromQuery] string searchByName, CancellationToken token)
    {
        var result = await _query.GetByFilterNameAsync(searchByName, token);
        return Ok(result);
    }
}