using AutoMapper;
using MediatR;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Domain.Dto;
using SoftPlex.Task.Core.Infrastructure.Data.Repositories;

namespace SoftPlex.Task.Api.Commands;

public class AddProductCommand: IRequest<ProductDto>
{
    public ProductPostDto Model { get; set; }
    
    
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, ProductDto>
    {
        private readonly IDbRepository<Product> _repository;
        private readonly IMapper _mapper;

        public AddProductCommandHandler(IDbRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(command.Model);
            product.Id = Guid.NewGuid();

            product = await _repository.Add(product, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}