using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Player _player;

    private void Start()
    {
        _player = GetComponentInParent<Player>();
        if (_player == null)
        {
            Debug.LogError("PlayerAnimations:: Player is null");
        }
    }

    public void AfterClimb()
    {
        _player.AfterClimb();
    }

    public void PlayerJump()
    {
        _player.StandingJump(true);
    }
    public void AfterStandingJump()
    {
        _player.StandingJump(false);
    }
}
