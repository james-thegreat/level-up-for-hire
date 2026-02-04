using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using TextRpg.Domain;
using TextRpg.Api; 


namespace TextRpg.Api.Controllers;



[ApiController]
[Route("api/[controller]")]
public class CombatsController : ControllerBase
{
    // Temporary in-memory "sessios"
    private static readonly Dictionary<Guid, Enemy> _enemies = new();

    private static readonly CombatService _combat = new();

    [HttpPost("start")]
    public ActionResult<StartCombatResponse> Start([FromBody] StartCombatRequest request)
    {
        // (Temporary) create an enemy; later we'll generate from tables
        var enemy = request.EnemyType?.ToLowerInvariant() switch
        {
            "goblin" => new Enemy("Goblin", maxHp: 10, attack: 4, defense: 0),
            "slime"  => new Enemy("Slime", maxHp: 8, attack: 3, defense: 1),
             _       => new Enemy("Traing Dummy", maxHp: 12, attack: 2, defense: 2)
        };

        var combatId = Guid.NewGuid();
        _enemies[combatId] = enemy;

        return Ok(new StartCombatResponse(combatId, enemy.Name, enemy.MaxHp, enemy.CurrentHp));
    }

    [HttpPost("{combatId:guid}/attack")]
    public ActionResult<AttackResponse> Attack(Guid combatId, [FromBody] AttackRequest request)
    {
        if (!_enemies.TryGetValue(combatId, out var enemy))
        {
            return NotFound("Combat not found. start a combat first.");
        }

        // Reuse the caracter store from CharacterController (temporary copy)
        // For now, re-create it here (next step we refacter into a repository/service).
        // Easiest quick fix: keep a shared storage class later.
        var charactersStore = CharactersStore.Characters;


        if (!charactersStore.TryGetValue(request.CharacterId, out var character))
        {
            return NotFound("Character not found.");
        }

        var result = _combat.Attack(character, enemy);

        // If combat ended, remove it
        if (result.EnemyDefeated || result.CharacterDefeated)
        {
            _enemies.Remove(combatId);
        }

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

public sealed record StartCombatRequest(string? EnemyType);
public sealed record StartCombatResponse(Guid CombatId, string EnemyName, int EnemyMaxHp, int EnemyCurrentHp);

public sealed record AttackRequest(Guid CharacterId);
public sealed record AttackResponse(
    int DamageToEnemy,
    int DamageToCharacter,
    int CharacterHpAfter,
    int EnemyHpAfter,
    bool EnemyDefeated,
    bool CharacterDefeated
);
