namespace ListingService.Shared;

public static class StringExtensions
{
    public static string GenerateSlug(this string content) => content.Replace(" " , "-");
}
