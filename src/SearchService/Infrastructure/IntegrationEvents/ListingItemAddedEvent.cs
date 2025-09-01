namespace ListingService.Infrastructure.IntegrationEvents;

public sealed record ListingItemAddedEvent
(
    Guid id,
    string title, 
    string description,
    string slug,
    string category,
    string imageUrl
);

