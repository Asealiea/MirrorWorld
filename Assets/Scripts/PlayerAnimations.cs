using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    private Player _player;
    [SerializeField] private float _ogCharConSize;
    [SerializeField] private Vector3 _ogCharConCenter;
    [SerializeField] private float _rollCharConSize;
    [SerializeField] private Vector3 _rollCharConCenter;

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

    public void StartRolling()
    {
        //change the char controller size and center 
 
        _player.StartingRoll(_rollCharConCenter, _rollCharConSize);
    }

    public void AfterRoll()
    {
        //reset the char controller 

        _player.AfterRoll(_ogCharConCenter, _ogCharConSize);

    }


}
