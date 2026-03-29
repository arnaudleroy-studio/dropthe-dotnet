using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DropThe;

/// <summary>
/// Content verticals supported by the DropThe knowledge graph.
/// Each vertical maps to a URL prefix on dropthe.org.
/// </summary>
public enum Vertical
{
    Movies,
    Series,
    Crypto,
    Companies,
    People,
    Travel,
    Tech,
    Gaming,
    Health,
    Gear
}

/// <summary>
/// Represents an entity in the DropThe knowledge graph.
/// Entities span movies, series, people, cryptocurrencies, companies, and more.
/// </summary>
public sealed record Entity(
    string Id,
    string Name,
    Vertical Vertical,
    string? Slug = null,
    int? Year = null,
    Dictionary<string, object>? Data = null)
{
    /// <summary>
    /// Returns the URL-safe slug for this entity. If no explicit slug was
    /// provided, one is derived from the name using <see cref="SlugHelper.Slugify"/>.
    /// </summary>
    public string EffectiveSlug => Slug ?? SlugHelper.Slugify(Name);
}

/// <summary>
/// Builds canonical URLs for DropThe entities and pages.
/// </summary>
public static class EntityUrl
{
    private const string Base = "https://dropthe.org";

    private static readonly Dictionary<Vertical, string> Prefixes = new()
    {
        [Vertical.Movies]    = "movies",
        [Vertical.Series]    = "series",
        [Vertical.Crypto]    = "crypto",
        [Vertical.Companies] = "companies",
        [Vertical.People]    = "people",
        [Vertical.Travel]    = "travel",
        [Vertical.Tech]      = "tech",
        [Vertical.Gaming]    = "gaming",
        [Vertical.Health]    = "health",
        [Vertical.Gear]      = "gear",
    };

    /// <summary>
    /// Returns the canonical detail page URL for an entity.
    /// Example: https://dropthe.org/movies/inception-2010
    /// </summary>
    public static string Detail(Entity entity)
    {
        var prefix = Prefixes[entity.Vertical];
        var slug   = entity.EffectiveSlug;
        return entity.Year.HasValue
            ? $"{Base}/{prefix}/{slug}-{entity.Year}"
            : $"{Base}/{prefix}/{slug}";
    }

    /// <summary>
    /// Returns the hub page URL for a given vertical.
    /// Example: https://dropthe.org/crypto
    /// </summary>
    public static string Hub(Vertical vertical) =>
        $"{Base}/{Prefixes[vertical]}";

    /// <summary>
    /// Builds a search URL with an optional vertical filter.
    /// </summary>
    public static string Search(string query, Vertical? vertical = null)
    {
        var encoded = Uri.EscapeDataString(query);
        var url = $"{Base}/search?q={encoded}";
        if (vertical.HasValue)
            url += $"&vertical={Prefixes[vertical.Value]}";
        return url;
    }
}

/// <summary>
/// Converts arbitrary text into URL-safe slugs following DropThe conventions.
/// Handles Unicode, diacritics, and special characters.
/// </summary>
public static class SlugHelper
{
    private static readonly Regex NonAlphaNum = new(@"[^a-z0-9\s-]", RegexOptions.Compiled);
    private static readonly Regex Whitespace  = new(@"[\s-]+",       RegexOptions.Compiled);

    /// <summary>
    /// Converts a string to a URL-safe slug.
    /// Removes diacritics, lowercases, strips non-alphanumerics, and collapses whitespace to hyphens.
    /// </summary>
    /// <example>
    /// SlugHelper.Slugify("The Dark Knight")   // "the-dark-knight"
    /// SlugHelper.Slugify("Cafe Mocha")        // "cafe-mocha"
    /// </example>
    public static string Slugify(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        // Normalize to decomposed form so diacritics become separate chars
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder(normalized.Length);

        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }

        var result = sb.ToString().Normalize(NormalizationForm.FormC);
        result = result.ToLowerInvariant();
        result = NonAlphaNum.Replace(result, "");
        result = Whitespace.Replace(result, "-");
        result = result.Trim('-');

        return result;
    }
}

/// <summary>
/// Package metadata and version information.
/// </summary>
public static class Info
{
    public const string Version = "0.1.0";
    public const string BaseUrl = "https://dropthe.org";
}
