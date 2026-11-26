
using UnityEngine;

public class AttackState : IGhostState
{
    private readonly GhostMovement movement;
    private readonly GhostAttack attack;

    public AttackState(
        GhostMovement movement,
        GhostAttack attack)
    {
        this.movement = movement;
        this.attack = attack;
    }

    public void Enter()
    {
        Debug.Log("Ghost State: Attack");

        movement.StopMovement();
    }

    public void Tick()
    {
        attack.TryAttack();
    }

    public void Exit()
    {
        movement.StopMovement();
    }
}
