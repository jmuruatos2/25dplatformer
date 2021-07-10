using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private GameObject _snapPoint, _standPoint;

    public float xCoord, yCoord, zCoord;
    public Vector3 StandPose;

    private void Start()
    {
        StandPose = _standPoint.transform.position;
        xCoord = _snapPoint.transform.position.x;
        yCoord = _snapPoint.transform.position.y;
        zCoord = _snapPoint.transform.position.z;
    }

}
