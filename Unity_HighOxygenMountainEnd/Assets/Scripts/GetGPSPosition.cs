﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetGPSPosition : MonoBehaviour
{
    public Text infoText;

    public bool forcePosition;
    public float forcedLatitude;
    public float forcedLongitude;
    public float forcedAltitude;

    private Vector3 originalPosition;
    private bool setOriginalValues = true;
    private float originalLatitude;
    private float originalLongitude;
    private float originalAltitude;

    public float currentLatitude;
    public float currentLongitude;
    public float currentAltitude;
    public bool hasLastData;
    public GPSPositionUtils.SLocationInfo lastData;

    void Awake()
    {
        hasLastData = false;
        //start GetCoordinate() function 
        StartCoroutine("GetCoordinates");
        //initialize target and original position
        originalPosition = transform.position;
    }

    IEnumerator GetCoordinates()
    {
        if(forcePosition)
        {
            hasLastData = true;

            originalLatitude = forcedLatitude;
            originalLongitude = forcedLongitude;
            originalAltitude = forcedAltitude;

            // overwrite current lat and long everytime
            currentLatitude = forcedLatitude;
            currentLongitude = forcedLongitude;
            currentAltitude = forcedAltitude;

            lastData.altitude = forcedAltitude;
            lastData.longitude = forcedLongitude;
            lastData.latitude = forcedLatitude;
        }

        //while true so this function keeps running once started.
        while (true)
        {
            // check if user has location service enabled
            if (!Input.location.isEnabledByUser)
                yield break;

            // Start service before querying location
            Input.location.Start(1f, .1f);

            // Wait until service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // Service didn't initialize in 20 seconds
            if (maxWait < 1)
            {
                print("Timed out");
                yield break;
            }

            // Connection has failed
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                print("Unable to determine device location");
                yield break;
            }
            else
            {
                // Access granted and location value could be retrieved
                print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude 
                    + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy 
                    + " " + Input.location.lastData.timestamp);

                //if original value has not yet been set save coordinates of player on app start
                if (setOriginalValues)
                {
                    originalLatitude = Input.location.lastData.latitude;
                    originalLongitude = Input.location.lastData.longitude;
                    originalAltitude = Input.location.lastData.altitude;

                    setOriginalValues = false;
                }

                // overwrite current lat and long everytime
                currentLatitude = Input.location.lastData.latitude;
                currentLongitude = Input.location.lastData.longitude;
                currentAltitude = Input.location.lastData.altitude;

                lastData.altitude = currentAltitude;
                lastData.longitude = currentLongitude;
                lastData.latitude = currentLatitude;

                hasLastData = true;

                infoText.text = currentLatitude + ", " + currentLongitude + ", " + currentAltitude;
            }
            Input.location.Stop();
        }
    }
}
