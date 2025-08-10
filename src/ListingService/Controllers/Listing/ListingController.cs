using ListingService.Controllers.Listing.Dtos;
using ListingService.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListingService.Controllers.Listing
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingController(ListingHandler listingHandler) : ControllerBase
    {
        private readonly ListingHandler _listingHandler = listingHandler;

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateListingRequestDto dto, CancellationToken cancellationToken)
        {
            await _listingHandler.Create(dto, cancellationToken);
            return Ok();
        }
    }
}
