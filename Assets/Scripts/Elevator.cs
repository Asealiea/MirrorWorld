using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool _down;
    [SerializeField] private Transform _bottom, _top;
    private float _speed = 2;



    // Update is called once per frame
    void FixedUpdate()
    {
        if (_down)
        {
            //currently at the top and want to move down
            transform.position = Vector3.MoveTowards(transform.position, _bottom.position, _speed * Time.deltaTime);

        }
        else
        {
            //currently not at the top and want to move to the top.
            transform.position = Vector3.MoveTowards(transform.position, _top.position, _speed * Time.deltaTime);
        }
    }

    public void CallElevator(bool call)
    {
        _down = call;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
            if (Input.GetKeyDown(KeyCode.E))
            {
                _down = !_down;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
