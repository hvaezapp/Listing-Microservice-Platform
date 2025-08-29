namespace ListingService.Infrastructure.IntegrationEvents;
public sealed record ListingItemAddedEvent
(
    string category, 
    string description
);

