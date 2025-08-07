using ListingService.Controllers.Listing.Dtos;
using ListingService.Domain.Entities;
using ListingService.Infrastructure.Persistence.Context;

namespace ListingService.Handlers;

public class ListingHandler
{
    private readonly ListingDBContext _listingDBContext;
    public ListingHandler(ListingDBContext listingDBContext)
    {
        _listingDBContext = listingDBContext;
    }

    public async Task Create(CreateListingRequestDto dto , CancellationToken cancellationToken)
    {
        // check validation
        // check slug duplication

        var newListing = Listing.Create(Guid.NewGuid(),
            dto.categoryId,
            dto.title,
            dto.description,
            dto.imageUrl
        );

        _listingDBContext.Add(newListing);
        await _listingDBContext.SaveChangesAsync(cancellationToken);


        // raise event (broker) to search service to create index
    }
}
