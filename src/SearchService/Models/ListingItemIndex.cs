using Elastic.Clients.Elasticsearch;

namespace SearchService.Models;

public class ListingItemIndex
{
    public const string IndexName = "listing-item-index";

    public required Id Id { get; set; }

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Slug { get; set; }
    public required string ListingCategory { get; set; }
    public required string ImageUrl { get; set; }

}
