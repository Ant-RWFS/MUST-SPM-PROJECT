using UnityEngine;

public class PlayerDieState : PlayerState
{
    public PlayerDieState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.stats.isInvisible = true;
        player.LockRB();
        NPCManager.instance.isPeace = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.UnlockRB();
        player.stats.isInvisible = false;
        NPCManager.instance.isPeace = false;

    }

    public override void Update()
    {
        base.Update();
    }
}
