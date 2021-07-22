using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private Transform _wayPoint1, _wayPoint2;
    [SerializeField] private float _speed = 2;
    [SerializeField] private bool _forward = true;




    void FixedUpdate()
    {
        if (_forward)
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPoint1.position, _speed * Time.deltaTime);
            if (transform.position == _wayPoint1.position)
            {
                _forward = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPoint2.position, _speed * Time.deltaTime);
            if (transform.position == _wayPoint2.position)
            {
                _forward = true;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //makes the players parent  this transform
            other.transform.parent = this.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //removes the players parent
        other.transform.parent = null;
    }

}
