using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Core.Infrastructure.Data.Repositories;

namespace SoftPlex.Task.Api.Queries;

public class ProductsQuery : IProductsQuery
{
    private readonly IMapper _mapper;
    private readonly IDbRepository<Product> _repository;
    public ProductsQuery(IMapper mapper, IDbRepository<Product> repository)
    {
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<ProductDto[]> GetAllAsync(CancellationToken cancel = default)
    {
        return await _repository.GetAll()
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancel);
    }

    public async Task<ProductDto[]> GetByFilterNameAsync(string? name, CancellationToken cancel = default)
    {
        var query = _repository.GetAll();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(x => x.Name.Contains(name));
        }
        
        return  await query
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancel);
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id, CancellationToken cancel = default)
    {
        var product = await _repository.GetAll()
            .FirstOrDefaultAsync(x => x.Id == id, cancel);

        return product is not null
            ? _mapper.Map<ProductDto>(product)
            : null;
    }
}