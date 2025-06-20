﻿namespace RentalApp.Application.Features.UserFeatures.GetUserById
{
    public sealed record GetUserByIdResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}