namespace ListingService.Controllers.Listing.Dtos;

public record CreateListingRequestDto(Guid categoryId , string title , string description , string imageUrl);

