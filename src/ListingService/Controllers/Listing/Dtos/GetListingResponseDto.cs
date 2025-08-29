namespace ListingService.Controllers.Listing.Dtos;

public sealed record GetListingResponseDto
(
    string title,
    string description,
    string slug, 
    string categoryTitle, 
    string imageUrl
);

