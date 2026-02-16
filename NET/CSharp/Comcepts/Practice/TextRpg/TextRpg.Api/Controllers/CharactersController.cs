using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using TextRpg.Api.Data;
using TextRpg.Domain;

namespace TextRpg.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombatsController : ControllerBase
{
    private readonly ICharacterRepository _characters;
    private readonly CombatService _combat;

    private static readonly ConcurrentDictionary<Guid, Enemy> _enemies = new();

    public CombatsController(
        ICharacterRepository characters,
        CombatService combat)
    {
        _characters = characters;
        _combat = combat;
    }

    [HttpPost("start")]
    public ActionResult<StartCombatResponse> Start([FromBody] StartCombatRequest request)
    {
        var enemy = request.EnemyType?.ToLowerInvariant() switch
        {
            "goblin" => new Enemy("Goblin", "/enemies/goblin.png", 10, 4, 0),
            "slime"  => new Enemy("Slime", "/enemies/slime.png", 8, 3, 1),
            _        => new Enemy("Training Dummy", "/enemies/dummy.png", 12, 2, 2)
        };

        var combatId = Guid.NewGuid();
        _enemies[combatId] = enemy;

        return Ok(new StartCombatResponse(
            combatId,
            enemy.Name,
            enemy.ImageUrl,
            enemy.MaxHp,
            enemy.CurrentHp
        ));
    }

    [HttpPost("{combatId:guid}/attack")]
    public ActionResult<AttackResponse> Attack(Guid combatId, [FromBody] AttackRequest request)
    {
        if (!_enemies.TryGetValue(combatId, out var enemy))
            return NotFound("Combat not found.");

        var character = _characters.GetById(request.CharacterId);
        if (character is null)
            return NotFound("Character not found.");

        var result = _combat.Attack(character, enemy);

        if (result.EnemyDefeated || result.CharacterDefeated)
            _enemies.TryRemove(combatId, out _);

        return Ok(new AttackResponse(
            result.DamageToEnemy,
            result.DamageToCharacter,
            result.CharacterHpAfter,
            result.EnemyHpAfter,
            enemy.ImageUrl,
            result.EnemyDefeated,
            result.CharacterDefeated
        ));
    }
}

public sealed record StartCombatRequest(string? EnemyType);

public sealed record StartCombatResponse(
    Guid CombatId,
    string EnemyName,
    string ImageUrl,
    int EnemyMaxHp,
    int EnemyCurrentHp
);

public sealed record AttackRequest(Guid CharacterId);

public sealed record AttackResponse(
    int DamageToEnemy,
    int DamageToCharacter,
    int CharacterHpAfter,
    int EnemyHpAfter,
    string ImageUrl,
    bool EnemyDefeated,
    bool CharacterDefeated
);
