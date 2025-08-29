namespace ListingService.Controllers.Listing.Dtos;

public sealed record CreateListingRequestDto
(
    Guid categoryId, 
    string title, 
    string description
);

