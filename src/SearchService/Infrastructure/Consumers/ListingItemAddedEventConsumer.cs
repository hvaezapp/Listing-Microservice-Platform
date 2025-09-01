using Elastic.Clients.Elasticsearch;
using ListingService.Infrastructure.IntegrationEvents;
using MassTransit;
using SearchService.Models;

namespace SearchService.Infrastructure.Consumers;

public class ListingItemAddedEventConsumer : IConsumer<ListingItemAddedEvent>
{
    private readonly ElasticsearchClient _elasticsearchClient;
    public ListingItemAddedEventConsumer(ElasticsearchClient elasticsearchClient)
    {
        _elasticsearchClient = elasticsearchClient;
    }

    public async Task Consume(ConsumeContext<ListingItemAddedEvent> context)
    {
        var message = context.Message;

        if (message is null) return;

        var item = new ListingItemIndex
        {
            Id = message.id.ToString(),
            Title = message.title,
            Description = message.description,
            Slug = message.slug,
            ListingCategory = message.category,
            ImageUrl = message.imageUrl,
        };

        var result = await _elasticsearchClient.Indices.ExistsAsync(ListingItemIndex.IndexName);

        if (!result.Exists)
        {
            await _elasticsearchClient.Indices.CreateAsync(index: ListingItemIndex.IndexName);
        }

        await _elasticsearchClient.IndexAsync(item, i => i.Index(ListingItemIndex.IndexName));

    }
}
