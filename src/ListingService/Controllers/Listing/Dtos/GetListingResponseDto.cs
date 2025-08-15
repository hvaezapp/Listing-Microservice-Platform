namespace ListingService.Controllers.Listing.Dtos;

public record GetListingResponseDto(string title  , string description , string slug , 
                                    string categoryTitle , string imageUrl);

