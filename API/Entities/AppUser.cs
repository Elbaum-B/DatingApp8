﻿namespace API.Entities;

public class AppUser
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PaswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}
