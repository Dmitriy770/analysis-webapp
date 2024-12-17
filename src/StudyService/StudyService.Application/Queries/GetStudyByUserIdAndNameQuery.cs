using MediatR;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Exceptions;
using StudyService.Domain.Models;

namespace StudyService.Application.Queries;

public record GetStudyByUserIdAndNameQuery(
    string DatasetName,
    long UserId)
    : IRequest<Study>;
    
internal sealed class GetStudyByUserIdAndNameHandler(
    IStudyRepository studyRepository)
    : IRequestHandler<GetStudyByUserIdAndNameQuery, Study>
{
    public async Task<Study> Handle(GetStudyByUserIdAndNameQuery request, CancellationToken cancellationToken)
    {
        var study = await studyRepository
            .GetByUserIdAsync(request.UserId, cancellationToken)
            .FirstOrDefaultAsync(study => study.Dataset.Name == request.DatasetName, cancellationToken);
        if (study is null)
        {
            throw new StudyNotFoundException(request.DatasetName);
        }
        
        return study;
    }
}