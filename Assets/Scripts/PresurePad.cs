using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePad : MonoBehaviour
{
    private float _distance;
    bool _used;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MoveableBox") && !_used)
        {
            _distance = Vector3.Distance(transform.position, other.transform.position);
            if (_distance < 0.3f)
            {
                other.attachedRigidbody.isKinematic = true;
                _used = true;
            }
        }
    }
}
