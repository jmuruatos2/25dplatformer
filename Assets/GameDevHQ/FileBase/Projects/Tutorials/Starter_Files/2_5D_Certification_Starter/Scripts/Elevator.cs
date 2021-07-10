using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private GameObject _positionA, _positionB;
    [SerializeField]
    private float _speed=0.1f;
    [SerializeField]
    private bool _goingUp;
    private bool _moviendo = true;


    // Update is called once per frame
    void Update()
    {

        if (_moviendo)
        {

            float step = _speed * Time.deltaTime;

            if (!_goingUp)
            {
                transform.position += new Vector3(0, -step, 0);
                if (transform.position.y <= _positionB.transform.position.y)
                {
                    _moviendo = false;
                    StartCoroutine( Cambio());
                }

            }
            else
            {
                transform.position += new Vector3(0, step, 0);
                if (transform.position.y >= _positionA.transform.position.y)
                {
                    _moviendo = false;
                   StartCoroutine( Cambio());
                }
            }
        }
    }

    IEnumerator Cambio()
    {
        yield return new WaitForSeconds(5.0f);

        _goingUp = !_goingUp;
        _moviendo = true;
    }
}
