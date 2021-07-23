using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private int _curentTarget;
    private int _speed = 3;
    private bool _down;
    private bool _breather;
    private WaitForSeconds _wait = new WaitForSeconds(5f);




    void FixedUpdate()
    {
        if (_waypoints.Count > 0 && _waypoints[_curentTarget] != null)
        {
            if (_waypoints.Count <= 1)
            {            
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_curentTarget].position, _speed * Time.deltaTime);
            if(transform.position == _waypoints[_curentTarget].position && !_breather)
            {
                //when target reached, wait for a couple seconds before moving on.
                _breather = true;
                StartCoroutine(WaitForBreather());

            }

        }

    }

    IEnumerator WaitForBreather()
    {
        yield return _wait;
        if (!_down)
        {
            _curentTarget++;
            if (_curentTarget == _waypoints.Count)
            {
                _curentTarget--;
                _down = true;
            }
        }
        else
        {
            _curentTarget--;
            if (_curentTarget < 0)
            {
                _curentTarget++;             
                _down = false;
            }
        }
        _breather = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
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