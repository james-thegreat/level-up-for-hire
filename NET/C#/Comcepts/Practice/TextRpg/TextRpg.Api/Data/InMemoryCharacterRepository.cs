using System.Collections.Concurrent;
using TextRpg.Domain;

namespace TextRpg.Api.Data;

public sealed class InMemoryCharacterRepository : ICharacterRepository
{
    private readonly ConcurrentDictionary<Guid, Character> _characters = new();

    public Character Add(Character character)
    {
        _characters[character.Id] = character;
        return character;
    }

    public Character? GetById(Guid id)
    {
        _characters.TryGetValue(id, out var character);
        return character;
    }
    
}
