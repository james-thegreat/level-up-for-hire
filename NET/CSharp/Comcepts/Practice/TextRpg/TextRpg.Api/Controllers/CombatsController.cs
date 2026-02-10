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

    // simple in-memory combat sessions
    private static readonly Dictionary<Guid, Enemy> _enemies = new();

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
            "goblin" => new Enemy("Goblin", maxHp: 10, attack: 4, defense: 0),
            "slime"  => new Enemy("Slime", maxHp: 8, attack: 3, defense: 1),
            _        => new Enemy("Training Dummy", maxHp: 12, attack: 2, defense: 2)
        };

        var combatId = Guid.NewGuid();
        _enemies[combatId] = enemy;

        return Ok(new StartCombatResponse(
            combatId,
            enemy.Name,
            enemy.MaxHp,
            enemy.CurrentHp
        ));
    }

    [HttpPost("{combatId:guid}/attack")]
    public ActionResult<AttackResponse> Attack(
        Guid combatId,
        [FromBody] AttackRequest request)
    {
        if (!_enemies.TryGetValue(combatId, out var enemy))
            return NotFound("Combat not found. Start a combat first.");

        var character = _characters.GetById(request.CharacterId);
        if (character is null)
            return NotFound("Character not found.");

        var result = _combat.Attack(character, enemy);

        // character HP changed — persist it
        _characters.Update(character);

        if (result.EnemyDefeated || result.CharacterDefeated)
            _enemies.Remove(combatId);

        return Ok(new AttackResponse(
            result.DamageToEnemy,
            result.DamageToCharacter,
            result.CharacterHpAfter,
            result.EnemyHpAfter,
            result.EnemyDefeated,
            result.CharacterDefeated
        ));
    }
}

/* ---------- DTOs (must exist) ---------- */

public sealed record StartCombatRequest(string? EnemyType);

public sealed record StartCombatResponse(
    Guid CombatId,
    string EnemyName,
    int EnemyMaxHp,
    int EnemyCurrentHp
);

public sealed record AttackRequest(Guid CharacterId);

public sealed record AttackResponse(
    int DamageToEnemy,
    int DamageToCharacter,
    int CharacterHpAfter,
    int EnemyHpAfter,
    bool EnemyDefeated,
    bool CharacterDefeated
);
