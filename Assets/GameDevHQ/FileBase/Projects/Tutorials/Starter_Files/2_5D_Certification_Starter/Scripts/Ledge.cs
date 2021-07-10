using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ledge : MonoBehaviour
{
    [SerializeField]
    private GameObject _snapPoint, _standPoint;

    public float xCoord, yCoord, zCoord;
    public Vector3 StandPose, snapPoint;

    private void Start()
    {
        StandPose = _standPoint.transform.position;
        snapPoint = _snapPoint.transform.position;
        xCoord = _snapPoint.transform.position.x;
        yCoord = _snapPoint.transform.position.y;
        zCoord = _snapPoint.transform.position.z;
        Debug.Log("snap position " + xCoord +" " + yCoord + " " + zCoord + " " + StandPose);
    }

    public Vector3 StandPoint()
    {
        return StandPose;
    }

    public Vector3 SnapPoint()
    {
        return snapPoint;
    }

}
