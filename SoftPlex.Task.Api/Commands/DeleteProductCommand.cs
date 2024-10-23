using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SoftPlex.Task.Core.Domain.Models;
using SoftPlex.Task.Core.Infrastructure.Data.Repositories;

namespace SoftPlex.Task.Api.Commands;

public class DeleteProductCommand : IRequest<bool>
{
    public Guid ModelId { get; set; }
    
    
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        public DeleteProductCommandHandler(IDbRepository<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        private readonly IDbRepository<Product> _repository;
        private readonly IMapper _mapper;

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = _repository.GetAll()
                .AsTracking()
                .FirstOrDefault(x => x.Id == request.ModelId);

            if (product is null) return false;
                
            await _repository.Remove(product, cancellationToken);
            return true;
        }
    }
}