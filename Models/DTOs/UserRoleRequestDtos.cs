using System;
using PointScore.Models;

public class UserRoleRequestCreateDto
{
    public int RoleId { get; set; }
}

public class UserRoleRequestProcessDto
{
    public bool Approve { get; set; }
}
public class RoleRequestDto
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public string? Email { get; set; }  // Changed from UserName to Email
    public int RoleId { get; set; }
    public string? RoleName { get; set; }
    public string? RoleDescription { get; set; }
    public string? Status { get; set; }
    public DateTime RequestedAt { get; set; }
    public DateTime? ProcessedAt { get; set; }
    public Guid? ProcessedBy { get; set; }
}