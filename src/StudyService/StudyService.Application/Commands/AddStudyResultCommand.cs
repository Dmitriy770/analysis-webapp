using MediatR;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;
using StudyService.Domain.Models.Studies;
using StudyService.Domain.Models.StudyResults;

namespace StudyService.Application.Commands;

public record AddStudyResultCommand(
    Guid StudyId,
    decimal[] Points)
    : IRequest;

internal sealed class AddStudyResultCommandHandler(
    IStudyRepository studyRepository,
    IStudyResultRepository studyResultRepository)
    : IRequestHandler<AddStudyResultCommand>
{
    public async Task Handle(AddStudyResultCommand request, CancellationToken cancellationToken)
    {
        if (await studyRepository.GetByIdAsync(request.StudyId, cancellationToken) is not { } study)
        {
            throw new StudyNotFoundException(request.StudyId.ToString());
        }

        if (study.Status == StudyStatus.Done)
        {
            throw new StudyAlreadyDoneException(request.StudyId.ToString());
        }
        
        var studyResult = new StudyResult(
            StudyId: study.Id,
            Points: request.Points);
        await studyResultRepository.AddAsync(studyResult, cancellationToken);
    }
}