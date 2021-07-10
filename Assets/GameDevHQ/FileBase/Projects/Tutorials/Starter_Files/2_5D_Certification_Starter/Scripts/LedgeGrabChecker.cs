using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabChecker : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Edge"))
        {

            Ledge ledge = other.gameObject.GetComponent<Ledge>();

            _player.EdgeGrab(ledge.xCoord, ledge.yCoord, ledge.zCoord, ledge.StandPose);
            
        }
    }
}
