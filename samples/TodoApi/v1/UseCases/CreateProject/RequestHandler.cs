using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NetCoreKit.Domain;
using NetCoreKit.Samples.TodoAPI.Extensions;

namespace NetCoreKit.Samples.TodoAPI.v1.UseCases.CreateProject
{
    public class RequestHandler : IRequestHandler<CreateProjectRequest, CreateProjectResponse>
    {
        private readonly IUnitOfWorkAsync _uow;

        public RequestHandler(IUnitOfWorkAsync uow)
        {
            _uow = uow;
        }

        public async Task<CreateProjectResponse> Handle(CreateProjectRequest request,
            CancellationToken cancellationToken)
        {
            var projectRepository = _uow.RepositoryAsync<Domain.Project>();

            var result = await projectRepository.AddAsync(Domain.Project.Load(request.Name));

            return new CreateProjectResponse {Result = result.ToDto()};
        }
    }
}
