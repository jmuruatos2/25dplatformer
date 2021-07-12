using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private GameObject _pointA, _pointB;
    private bool _toPointA;
    [SerializeField]
    private float _speed = 5.0f;
    private float _step;
    void FixedUpdate()
    {
        _step = _speed * Time.deltaTime;

        if (!_toPointA)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.transform.position, _step);
            if (transform.position == _pointB.transform.position)
            {
                _toPointA = !_toPointA;
            }
        }

        if (_toPointA)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.transform.position, _step);
            if (transform.position == _pointA.transform.position)
            {
                _toPointA = !_toPointA;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
   
        if (other.CompareTag("Player"))
        {

            other.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
