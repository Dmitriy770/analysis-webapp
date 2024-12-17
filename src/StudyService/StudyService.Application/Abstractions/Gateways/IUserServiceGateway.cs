using Refit;
using StudyService.Application.Abstractions.Gateways.Models;

namespace StudyService.Application.Abstractions.Gateways;

public interface IUserServiceGateway
{
    [Get("/internal/user/{userId}/limits/total")]
    public Task<Limit> GetUserLimits(long userId);
}