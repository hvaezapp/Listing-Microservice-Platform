using ListingService.Dtos;
using ListingService.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ListingService.Controllers.api.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ListingController(ListingHandler listingHandler) : ControllerBase
    {
        private readonly ListingHandler _listingHandler = listingHandler;

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _listingHandler.GetAll(cancellationToken));
        }


        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateListingRequestDto dto, CancellationToken cancellationToken)
        {
            await _listingHandler.Create(dto, cancellationToken);
            return Ok();
        }



    }
}
