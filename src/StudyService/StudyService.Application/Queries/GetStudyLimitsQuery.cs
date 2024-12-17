using MediatR;
using StudyService.Application.Abstractions.Gateways;
using StudyService.Application.Abstractions.Providers;
using StudyService.Application.Abstractions.Repositories;
using StudyService.Domain.Models.Limits;

namespace StudyService.Application.Queries;

public record GetStudyLimitsQuery(
    long UserId)
    : IRequest<StudyLimits>;
    
internal sealed class GetStudyLimitsQueryHandler(
    IUserServiceGateway userServiceGateway,
    IStudyRepository studyRepository,
    IDateTimeProvider dateTimeProvider)
    : IRequestHandler<GetStudyLimitsQuery, StudyLimits>
{
    public async Task<StudyLimits> Handle(GetStudyLimitsQuery request, CancellationToken cancellationToken)
    {
        var limit = await userServiceGateway.GetUserLimits(request.UserId);
        
        var now = dateTimeProvider.Now;
        var studies = await studyRepository
            .GetByUserIdAsync(request.UserId, cancellationToken)
            .OrderByDescending(study => study.CreationDate)
            .Where(study => study.CreationDate >= now - Timeout)
            .ToListAsync(cancellationToken);

        var left = Math.Max(limit.Total - studies.Count, 0);
        var reducesAt = studies.Last().CreationDate;

        return new StudyLimits(
            Total: limit.Total,
            Left: left,
            ReducesAt: reducesAt);
    }
    
    private static readonly TimeSpan Timeout = TimeSpan.FromHours(1);
}