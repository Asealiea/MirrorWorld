using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
    [SerializeField] private MeshRenderer _button;
    [SerializeField] private int _requiredCoins = 8;
    private Elevator _ele;

    private void Start()
    {
        _ele = GameObject.Find("Elevator").GetComponent<Elevator>();
        if (_ele == null)
        {
            Debug.Log("ElevatorPanel:: Elevator is null");
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        

            if (Input.GetKeyDown(KeyCode.E) && other.GetComponent<Player>().CoinsCount() >= _requiredCoins)
            {
                //turn the light blue.
                _button.material.color = Color.green;
                _ele.CallElevator(true);
             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _button.material.color = Color.red;
            _ele.CallElevator(false);
        }
    }
}
