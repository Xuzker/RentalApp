namespace RentalApp.Application.Features.ApartmentFeatures.DeleteApartment
{
    public sealed record DeleteApartmentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public int Rooms { get; set; }
        public decimal PricePerDay { get; set; }
        public DateTimeOffset DateCreated { get; set; }
    }
}