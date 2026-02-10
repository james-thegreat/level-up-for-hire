using Microsoft.AspNetCore.Mvc;
using TextRpg.Api.Data;
using TextRpg.Domain;

namespace TextRpg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CharactersController : ControllerBase
{
    private readonly ICharacterRepository _repository;

    public CharactersController(ICharacterRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public ActionResult<CharacterDto> Create([FromBody] CreateCharacterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required.");

        var character = new Character(request.Name);
        _repository.Add(character);

        return CreatedAtAction(
            nameof(GetById),
            new { id = character.Id },
            CharacterDto.From(character)
        );
    }

    [HttpGet("{id:guid}")]
    public ActionResult<CharacterDto> GetById(Guid id)
    {
        var character = _repository.GetById(id);
        if (character is null)
            return NotFound();

        return Ok(CharacterDto.From(character));
    }
}

public sealed record CreateCharacterRequest(string Name);

public sealed record CharacterDto(
    Guid Id,
    string Name,
    int Level,
    int MaxHp,
    int CurrentHp,
    int Attack,
    int Defense)
{
    public static CharacterDto From(Character c) =>
        new(c.Id, c.Name, c.Level, c.MaxHp, c.CurrentHp, c.Attack, c.Defense);
}
