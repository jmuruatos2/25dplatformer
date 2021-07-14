using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField]
    private Transform _ladderBottom, _ladderTop, _landingTop, _landingBottom;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Player es null");
            }
            _player.SetLadderAvailable(true,_ladderBottom.transform.position,_ladderTop.transform.position,_landingTop.transform.position,_landingBottom.transform.position);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player _player = other.gameObject.GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError("Player es null");
            }
            _player.SetLadderAvailable(false, _ladderBottom.transform.position, _ladderTop.transform.position,_landingTop.transform.position,_landingBottom.transform.position);
        }
    }
}
