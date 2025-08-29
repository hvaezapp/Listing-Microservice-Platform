using ListingService.Controllers.Listing.Dtos;
using ListingService.Domain.Entities;
using ListingService.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ListingService.Handlers;

public class ListingHandler
{
    private readonly ListingDBContext _listingDBContext;
    private readonly CurrentUserHandler _currentUserHandler;

    public ListingHandler(ListingDBContext listingDBContext, CurrentUserHandler currentUserHandler)
    {
        _listingDBContext = listingDBContext;
        _currentUserHandler = currentUserHandler;
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

        // raise event (broker) to search service to create index
    }
}
