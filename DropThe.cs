using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DropThe
{
    /// <summary>
    /// Core metadata and constants for the DropThe data platform.
    /// Homepage: https://dropthe.org
    /// </summary>
    public static class Info
    {
        public const string Version = "0.1.0";
        public const string BaseUrl = "https://dropthe.org";
        public const string ApiBase = "https://ksrbnzyyyzanqmqmfvtx.supabase.co/rest/v1";
        public const string Organization = "DropThe";
        public const string Description = "Data utility media network covering movies, series, crypto, companies, and more.";

        /// <summary>
        /// Supported entity types in the DropThe knowledge graph.
        /// </summary>
        public static readonly string[] EntityTypes = new[]
        {
            "movies", "series", "people", "cryptocurrencies", "companies"
        };

        /// <summary>
        /// Supported verticals on dropthe.org.
        /// </summary>
        public static readonly string[] Verticals = new[]
        {
            "tech", "coin", "money", "culture", "travel", "data", "gear", "gaming", "health", "series", "stream"
        };
    }

    /// <summary>
    /// Represents a single entity in the DropThe knowledge graph.
    /// </summary>
    public class Entity
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public Dictionary<string, JsonElement>? Data { get; set; }

        public override string ToString() => $"{Name} ({Type})";
    }

    /// <summary>
    /// Slug generation and URL utilities for DropThe content.
    /// </summary>
    public static class SlugHelper
    {
        /// <summary>
        /// Generates a URL-safe slug from a string.
        /// Matches the slug format used on dropthe.org.
        /// </summary>
        public static string GenerateSlug(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var slug = input.ToLowerInvariant().Trim();
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", "-");
            slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");
            return slug.Trim('-');
        }

        /// <summary>
        /// Builds a full URL for an entity on dropthe.org.
        /// </summary>
        public static string BuildEntityUrl(string type, string slug)
        {
            return $"{Info.BaseUrl}/{type}/{slug}/";
        }

        /// <summary>
        /// Builds a full URL for a vertical page.
        /// </summary>
        public static string BuildVerticalUrl(string vertical)
        {
            return $"{Info.BaseUrl}/{vertical}/";
        }
    }

    /// <summary>
    /// Tier classification for entities in the DropThe knowledge graph.
    /// </summary>
    public static class TierSystem
    {
        /// <summary>
        /// Classification tiers from S (highest) to D (lowest).
        /// </summary>
        public static readonly Dictionary<string, string> Tiers = new()
        {
            { "S", "Exceptional - Top 1% entities with maximum coverage" },
            { "A", "Excellent - Well-known entities with strong data" },
            { "B", "Good - Notable entities with solid coverage" },
            { "C", "Average - Entities with basic information" },
            { "D", "Minimal - Entities with limited data" }
        };

        /// <summary>
        /// Returns the tier description for a given tier letter.
        /// </summary>
        public static string GetDescription(string tier)
        {
            return Tiers.TryGetValue(tier.ToUpperInvariant(), out var desc) ? desc : "Unknown tier";
        }
    }

    /// <summary>
    /// Vertical color definitions matching the DropThe brand system.
    /// </summary>
    public static class VerticalColors
    {
        public static readonly Dictionary<string, string> Colors = new()
        {
            { "research", "#c05c32" },
            { "tech", "#007BFF" },
            { "coin", "#F7931A" },
            { "money", "#00C853" },
            { "culture", "#E83E8C" },
            { "travel", "#069494" },
            { "data", "#069494" },
            { "gear", "#FD7E14" },
            { "gaming", "#9146FF" },
            { "health", "#E63946" },
            { "series", "#8B5CF6" },
            { "companies", "#D4A574" },
            { "stream", "#10B981" }
        };

        /// <summary>
        /// Gets the hex color for a vertical, or null if not found.
        /// </summary>
        public static string? GetColor(string vertical)
        {
            return Colors.TryGetValue(vertical.ToLowerInvariant(), out var color) ? color : null;
        }
    }
}
