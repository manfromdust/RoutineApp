using Expedition178.Characters;

namespace Expedition178.Interfaces;

public interface IBattle
{
/// <summary>
/// Performs a battle between the player's team of adventurers and a team of enemy creatures.
/// </summary>
/// <param name="player">The player's team</param>
/// <param name="enemy">The enemy team</param>
/// <returns>The winner</returns>
public Character[] PerformBattle(Adventurer[] player, Monster[] enemy);

/// <summary>
/// Performs one round of battle between two characters.
/// </summary>
/// <param name="adventurer">The player's adventurer</param>
/// <param name="creature">The enemy creature</param>
/// <returns>The character that wins the round</returns>
public Character? PerformRound(Adventurer adventurer, Monster creature);
}