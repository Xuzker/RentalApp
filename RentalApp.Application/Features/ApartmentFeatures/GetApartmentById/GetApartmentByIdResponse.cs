namespace RentalApp.Application.Features.ApartmentFeatures.GetApartmentById
{
    public sealed record GetApartmentByIdResponse(
        Guid Id,
        string Title,
        string Description,
        string Address,
        int Rooms,
        decimal PricePerDay,
        bool IsAvailable,
        DateTimeOffset DateCreated);
}