namespace ListingService.Dtos;

public sealed record CreateListingRequestDto
(
    Guid categoryId, 
    string title, 
    string description
);

