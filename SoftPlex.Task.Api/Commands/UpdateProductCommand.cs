using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Core.Infrastructure.Data.Repositories;

namespace SoftPlex.Task.Api.Commands;

public class UpdateProductCommand : IRequest<ProductDto?>
{
    public ProductDto Model { get; set; }
    
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto?>
    {
        public UpdateProductCommandHandler(IDbRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        private readonly IDbRepository<Product> _repository;
        private readonly IMapper _mapper;

        public async Task<ProductDto?> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            if (!await _repository.GetAll().AnyAsync(x => x.Id == command.Model.Id, cancellationToken)) return null;
            
            var product = _mapper.Map<Product>(command.Model);
            product = await _repository.Update(product, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}