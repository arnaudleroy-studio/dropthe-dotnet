# DropThe .NET Client

Official .NET library for the [DropThe](https://dropthe.org) data platform -- a data utility media network covering movies, series, cryptocurrencies, companies, and 1.8 million+ entities in a connected knowledge graph.

## Installation

```bash
dotnet add package DropThe
```

Or via the NuGet Package Manager:

```
Install-Package DropThe
```

## Quick Start

```csharp
using DropThe;

// Access platform metadata
Console.WriteLine($"Platform: {Info.Organization}");
Console.WriteLine($"Version:  {Info.Version}");
Console.WriteLine($"API Base: {Info.ApiBase}");

// List all supported entity types
foreach (var type in Info.EntityTypes)
    Console.WriteLine($"  - {type}");
```

## Features

### Slug Generation

Generate URL-safe slugs that match the format used on dropthe.org:

```csharp
using DropThe;

var slug = SlugHelper.GenerateSlug("The Dark Knight Rises");
// Output: "the-dark-knight-rises"

var url = SlugHelper.BuildEntityUrl("movies", slug);
// Output: "https://dropthe.org/movies/the-dark-knight-rises/"

var verticalUrl = SlugHelper.BuildVerticalUrl("coin");
// Output: "https://dropthe.org/coin/"
```

### Tier Classification

DropThe uses a five-tier system (S through D) to classify entity data quality and coverage:

```csharp
using DropThe;

// Get all tier definitions
foreach (var (tier, desc) in TierSystem.Tiers)
    Console.WriteLine($"Tier {tier}: {desc}");

// Look up a specific tier
var description = TierSystem.GetDescription("A");
// Output: "Excellent - Well-known entities with strong data"
```

### Vertical Colors

Access the official brand color palette for each content vertical:

```csharp
using DropThe;

var techColor = VerticalColors.GetColor("tech");
// Output: "#007BFF"

var coinColor = VerticalColors.GetColor("coin");
// Output: "#F7931A"

// Iterate all verticals
foreach (var (vertical, hex) in VerticalColors.Colors)
    Console.WriteLine($"{vertical}: {hex}");
```

### Entity Model

Work with entities from the DropThe knowledge graph:

```csharp
using DropThe;

var entity = new Entity
{
    Id = "abc-123",
    Name = "Bitcoin",
    Slug = "bitcoin",
    Type = "cryptocurrencies"
};

Console.WriteLine(entity); // Output: "Bitcoin (cryptocurrencies)"
```

## Data Coverage

The DropThe knowledge graph includes:

| Category | Count |
|----------|-------|
| Entities | 1,800,000+ |
| Entity Links | 2,900,000+ |
| Movies & Series | 66,000+ |
| Streaming Records | 685,000+ |
| Aliases | 80,000+ |
| Geographic Entities | 2,400+ |

## Verticals

DropThe organizes content across these verticals: Tech, Coin, Money, Culture, Travel, Data, Gear, Gaming, Health, Series, Stream, and Companies.

## Links

- [DropThe Homepage](https://dropthe.org)
- [Source Code](https://github.com/arnaudleroy-studio/dropthe-dotnet)
- [Report Issues](https://github.com/arnaudleroy-studio/dropthe-dotnet/issues)

## License

MIT License. See [LICENSE](LICENSE) for details.
