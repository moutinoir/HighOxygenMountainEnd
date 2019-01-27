using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointNorth : MonoBehaviour
{
    void Update()
    {
        // Orient an object to point to magnetic north.
        transform.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);
    }
}
