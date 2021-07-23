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




    void Update()
    {
        if (_waypoints.Count > 0 && _waypoints[_curentTarget] != null)
        {
            if (_waypoints.Count <= 1)
            {            
                return;
            }

            transform.position = Vector3.MoveTowards(transform.position, _waypoints[_curentTarget].position, _speed * Time.deltaTime);
          //  float distance = Vector3.Distance(transform.position, _waypoints[_curentTarget].position);
            //if (distance < 1 && !_breather)
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

}



/*


public class GuardAI : MonoBehaviour
{
    [SerializeField] private List <Transform> _waypoints;
    [SerializeField] private int _curentTarget;

    private bool _down;
    private bool _breather;
 



    void Update()
    {
        if (_waypoints.Count > 0 && _waypoints[_curentTarget] != null)
        {
            if (_waypoints.Count <= 1)
            {
                _anim.SetBool("Walking", false);
                return;
            }

            _agent.SetDestination(_waypoints[_curentTarget].position);
            float distance = Vector3.Distance(transform.position,_waypoints[_curentTarget].position);
            if (distance < 1 && !_breather)
            {
                //when target reached, wait for a couple seconds before moving on.
                if (_anim != null)
                    _anim.SetBool("Walking", false);                
                _breather = true;
                StartCoroutine(WaitForBreather());

            }
            
        }

    }

    IEnumerator WaitForBreather()
    {
        //yield return new WaitForSeconds(Random.Range(2f, 5f));
        if (!_reverse)
        {
            _curentTarget++;
            if (_curentTarget == _waypoints.Count)
            {
                _curentTarget--;
                yield return new WaitForSeconds(Random.Range(2f, 5f));
                _reverse = true;
            }
        }
        else
        {
            _curentTarget--;
            if (_curentTarget < 0)
            {
                _curentTarget++;
                yield return new WaitForSeconds(Random.Range(2f, 5f));
                _reverse = false;
            }
        }
        if (_anim != null)
        _anim.SetBool("Walking", true);                
        _breather = false;
    }

}
*/
