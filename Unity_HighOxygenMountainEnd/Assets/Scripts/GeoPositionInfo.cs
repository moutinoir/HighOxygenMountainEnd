using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoPositionInfo : MonoBehaviour
{
    public TextMesh text;

    //private float originalLatitude;
    //private float originalLongitude;
    //private float currentLongitude;
    //private float currentLatitude;

    //private GameObject distanceTextObject;
    //private double distance;

    //private bool setOriginalValues = true;

    //private Vector3 targetPosition;
    //private Vector3 originalPosition;

    //private float speed = .1f;

    //static IEnumerator GetCoordinates(GeoPositionInfo geoPositionInfo)
    //{
    //    //while true so this function keeps running once started.
    //    while (true)
    //    {
    //        // check if user has location service enabled
    //        if (!Input.location.isEnabledByUser)
    //            yield break;

    //        // Start service before querying location
    //        Input.location.Start(1f, .1f);

    //        // Wait until service initializes
    //        int maxWait = 20;
    //        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
    //        {
    //            yield return new WaitForSeconds(1);
    //            maxWait--;
    //        }

    //        // Service didn't initialize in 20 seconds
    //        if (maxWait < 1)
    //        {
    //            print("Timed out");
    //            yield break;
    //        }

    //        // Connection has failed
    //        if (Input.location.status == LocationServiceStatus.Failed)
    //        {
    //            print("Unable to determine device location");
    //            yield break;
    //        }
    //        else
    //        {
    //            // Access granted and location value could be retrieved
    //            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);

    //            //if original value has not yet been set save coordinates of player on app start
    //            if (geoPositionInfo.setOriginalValues)
    //            {
    //                geoPositionInfo.originalLatitude = Input.location.lastData.latitude;
    //                geoPositionInfo.originalLongitude = Input.location.lastData.longitude;
    //                geoPositionInfo.setOriginalValues = false;
    //            }

    //            //overwrite current lat and lon everytime
    //            geoPositionInfo.currentLatitude = Input.location.lastData.latitude;
    //            geoPositionInfo.currentLongitude = Input.location.lastData.longitude;
    //        }
    //        Input.location.Stop();
    //    }
    //}

    //calculates distance between two sets of coordinates, taking into account the curvature of the earth.
    //static public float Calc(float lat1, float lon1, float lat2, float lon2)
    //{
        //float distance = 0;
        //float R = 6378.137f; // Radius of earth in KM
        //float dLat = lat2 * Mathf.PI / 180 - lat1 * Mathf.PI / 180;
        //float dLon = lon2 * Mathf.PI / 180 - lon1 * Mathf.PI / 180;
        //float a = Mathf.Sin(dLat / 2) * Mathf.Sin(dLat / 2) +
        //  Mathf.Cos(lat1 * Mathf.PI / 180) * Mathf.Cos(lat2 * Mathf.PI / 180) *
        //  Mathf.Sin(dLon / 2) * Mathf.Sin(dLon / 2);
        //float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        //distance = R * c;
        //distance = distance * 1000f; // meters
        //                             //set the distance text on the canvas
        //distanceTextObject.GetComponent<Text>().text = "Distance: " + distance;
        ////convert distance from double to float
        //float distanceFloat = (float)distance;
        ////set the target position of the ufo, this is where we lerp to in the update function
        //targetPosition = originalPosition - new Vector3(0, 0, distanceFloat * 12);
        ////distance was multiplied by 12 so I didn't have to walk that far to get the UFO to show up closer

        //return distance;
    //}
}