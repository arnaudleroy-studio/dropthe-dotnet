# DropThe .NET

Entity modeling, URL generation, and slug utilities for the [DropThe](https://dropthe.org) knowledge graph. DropThe indexes 1.8 million entities across movies, series, cryptocurrencies, companies, and people, connected by nearly 3 million relationship links.

## Installation

```bash
dotnet add package DropThe
```

## Quick Start

Create an entity, generate its canonical URL, and slugify arbitrary text in a few lines:

```csharp
using DropThe;

// Model an entity from the knowledge graph
var inception = new Entity(
    Id: "mov_inception",
    Name: "Inception",
    Vertical: Vertical.Movies,
    Year: 2010);

Console.WriteLine(EntityUrl.Detail(inception));
// => https://dropthe.org/movies/inception-2010

Console.WriteLine(EntityUrl.Hub(Vertical.Crypto));
// => https://dropthe.org/crypto

Console.WriteLine(EntityUrl.Search("bitcoin", Vertical.Crypto));
// => https://dropthe.org/search?q=bitcoin&vertical=crypto
```

## Slug Generation

`SlugHelper.Slugify` handles Unicode normalization, diacritics removal, and whitespace collapsing so you get clean, URL-safe slugs every time:

```csharp
SlugHelper.Slugify("The Dark Knight Rises");  // "the-dark-knight-rises"
SlugHelper.Slugify("Cafe Mocha & Espresso");  // "cafe-mocha--espresso"
SlugHelper.Slugify("  Leading Spaces  ");     // "leading-spaces"
```

## Verticals

DropThe organizes content into verticals, each with its own hub page and entity namespace:

| Vertical | Hub URL |
|----------|---------|
| Movies | `/movies` |
| Series | `/series` |
| Crypto | `/crypto` |
| Companies | `/companies` |
| People | `/people` |
| Travel | `/travel` |
| Tech | `/tech` |
| Gaming | `/gaming` |
| Health | `/health` |
| Gear | `/gear` |

## API Surface

| Type | Description |
|------|-------------|
| `Entity` | Immutable record representing a knowledge graph entity |
| `Vertical` | Enum of supported content verticals |
| `EntityUrl` | Static methods for building canonical DropThe URLs |
| `SlugHelper` | Unicode-aware slug generation |

## Links

- [DropThe](https://dropthe.org)
- [Source Code](https://github.com/arnaudleroy-studio/dropthe-dotnet)
- [NuGet Package](https://www.nuget.org/packages/DropThe)

## License

MIT License. See [LICENSE](LICENSE) for details.
