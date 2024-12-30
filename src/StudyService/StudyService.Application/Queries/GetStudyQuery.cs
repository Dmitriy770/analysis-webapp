using MediatR;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;

namespace StudyService.Application.Queries;

public record GetStudyQuery(
    Guid StudyId,
    long UserId)
    : IRequest<Study>;
    
internal sealed class GetStudyQueryHandler(
    IStudyRepository studyRepository)
    : IRequestHandler<GetStudyQuery, Study>
{
    public async Task<Study> Handle(GetStudyQuery request, CancellationToken cancellationToken)
    {
        if (await studyRepository.GetByIdAsync(request.StudyId, cancellationToken) is not { } study)
        {
            throw new StudyNotFoundException(request.StudyId.ToString());
        }

        if (study.UserId != request.UserId)
        {
            throw new StudyNotFoundException(request.StudyId.ToString());
        }
        
        return study;
    }
}