using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPosition : MonoBehaviour
{
    public GetGPSPosition gpsPosition;
    public static Text debug;
    public Text debugText;

    private void Awake()
    {
        debug = debugText;
    }
}
