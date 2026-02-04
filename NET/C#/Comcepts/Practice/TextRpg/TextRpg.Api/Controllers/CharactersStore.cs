using System.Collections.Concurrent;
using TextRpg.Domain;

namespace TextRpg.Api;

public static class CharactersStore
{
    public static ConcurrentDictionary<Guid, Character> Characters { get; } = new();
}
