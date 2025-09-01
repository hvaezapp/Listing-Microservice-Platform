using ListingService.Domain.Entities;
using ListingService.Dtos;
using ListingService.Infrastructure.IntegrationEvents;
using ListingService.Infrastructure.Persistence.Context;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace ListingService.Handlers;

public class ListingHandler
{
    private readonly ListingDBContext _listingDBContext;
    private readonly CurrentUserHandler _currentUserHandler;
    private readonly IPublishEndpoint _publishEndpoint;

    public ListingHandler(ListingDBContext listingDBContext, 
                          CurrentUserHandler currentUserHandler,
                          IPublishEndpoint publishEndpoint)
    {
        _listingDBContext = listingDBContext;
        _currentUserHandler = currentUserHandler;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<IEnumerable<GetListingResponseDto>> GetAll(CancellationToken cancellationToken)
    {
        return (await _listingDBContext.Listings
                                       .Include(i => i.Category)
                                       .Select(s => new GetListingResponseDto
                                        (
                                            s.Title,
                                            s.Description, 
                                            s.Slug,
                                            s.Category.Title , 
                                            s.ImageUrl

                                        )).ToListAsync(cancellationToken));
    }
    public async Task Create(CreateListingRequestDto dto, CancellationToken cancellationToken)
    {
        // check validation
        // check slug duplication

        var newListing = Listing.Create(_currentUserHandler.UserId, 
            dto.categoryId,
            dto.title,
            dto.description
        );

        _listingDBContext.Add(newListing);
        await _listingDBContext.SaveChangesAsync(cancellationToken);

        // get added listing from db
        var loadListingItem = await _listingDBContext.Listings
                                                 .Include(l => l.Category)
                                                 .FirstAsync(l => l.Slug == newListing.Slug,
                                                 cancellationToken);

        // raise event for search service to create index
        await _publishEndpoint.Publish(new ListingItemAddedEvent(

                    loadListingItem.Id,
                    loadListingItem.Title,
                    loadListingItem.Description,
                    loadListingItem.Slug,
                    loadListingItem.Category.Title,
                    loadListingItem.ImageUrl

              ));
    }
}
