using MediatR;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;
using StudyService.Domain.Models.StudyResults;

namespace StudyService.Application.Queries;

public record GetStudyResultQuery(
    Guid StudyId,
    long UserId)
    : IRequest<StudyResult>;

internal sealed class GetStudyResultQueryHandler(
    IStudyResultRepository studyResultRepository,
    IStudyRepository studyRepository)
    : IRequestHandler<GetStudyResultQuery, StudyResult>
{
    public async Task<StudyResult> Handle(GetStudyResultQuery request, CancellationToken cancellationToken)
    {
        if (await studyResultRepository.GetAsync(request.StudyId, cancellationToken) is not { } studyResult)
        {
            throw new StudyResultNotFoundException(request.StudyId);
        }

        if (await studyRepository.GetByIdAsync(studyResult.StudyId, cancellationToken) is not { } study)
        {
            throw new StudyResultNotFoundException(request.StudyId);
        }

        if (study.UserId != request.UserId)
        {
            throw new StudyNotFoundException(request.StudyId.ToString());
        }
        
        return studyResult;
    }
}