﻿namespace Uis.Infrastructure.Repositories.Users.Models;

internal sealed class UserStorageRecord
{
    public long Id {get; set;}
    public string Login  {get; set;} = string.Empty;
    public string Name {get; set;} = string.Empty;
    public string AvatarUri { get; set; } = string.Empty;
    public int Limit {get; set;}
}