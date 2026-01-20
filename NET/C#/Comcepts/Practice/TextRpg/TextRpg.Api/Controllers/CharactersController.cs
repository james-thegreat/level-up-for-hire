using Microsoft.AspNetCore.Mvc;
using TextRpg.Domain;

namespace TextRpg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController: CorntrollerBase
{
    // For now: in-memory storage (we'll replace later)
    private static readonly Dictionary<Guid, Character> _characters = new();

    [HttpPosst]
    public ActionResult<CharacterDto> Create([FromBody] CreateCharacterRequest)
    {
        var character = new Character(request.Name);
        _characters[character.Id] = character;

        return CharacterAtAction(nameof(GetById), new { id = character.Id }, CharacterDto.From(character));
    }

    [HttpGet("{id:guid}")]
    public ActionResult<CharacterDto> GetById(Guid id)
    {
        if (!_characters.TryGetValue(id, out var character))
        {
            return NotFound();
        }

        return OK(CharacterDto.From(character));
    }
}

public sealed record CreateCharacterRequest(string Name);

public sealed record CharacterDto(Guid Id, string Name, intLevel, int MaxHp, int CurrentHp, int Attack, int Defense)
{
    public static CharacterDto From(Character c) =>
        new(c.Id, c.Name, c.Level, c.MaxHp, c.CurrentHp, c.Attack, c.Defense);
}
