namespace RentalApp.Application.Features.UserFeatures.GetUserByEmail
{
    public sealed record GetUserByEmailResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}