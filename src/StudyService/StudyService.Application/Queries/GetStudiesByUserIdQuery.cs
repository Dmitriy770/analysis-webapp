using MediatR;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;

namespace StudyService.Application.Queries;

public record GetStudiesByUserIdQuery(
    long UserId)
    : IStreamRequest<Study>;

internal sealed class GetStudyByUserIdQueryHandler(
    IStudyRepository studyRepository)
    : IStreamRequestHandler<GetStudiesByUserIdQuery, Study>
{
    public IAsyncEnumerable<Study> Handle(GetStudiesByUserIdQuery request, CancellationToken cancellationToken)
    {
        return studyRepository.GetByUserIdAsync(request.UserId, cancellationToken);
    }
}