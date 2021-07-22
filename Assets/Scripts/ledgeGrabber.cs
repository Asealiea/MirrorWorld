using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ledgeGrabber : MonoBehaviour
{
    [SerializeField] private Transform _handsPos;
    [SerializeField] private Transform _endPos;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Ledge"))
        {

            Player player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.GrabLedge(_handsPos,_endPos);
            }
        }
    }
}
