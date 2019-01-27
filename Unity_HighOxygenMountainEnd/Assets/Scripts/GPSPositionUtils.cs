using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPSPositionUtils : MonoBehaviour
{
    private static float earthRadius = 6371000;
    private static Transform north;

    public Transform utilNorth;

    private void Awake()
    {
        north = utilNorth;
    }

    [System.Serializable]
    public struct SLocationInfo
    {
        public float latitude;
        public float longitude;
        public float altitude;

        public SLocationInfo(LocationInfo location)
        {
            altitude = location.altitude;
            longitude = location.longitude;
            latitude = location.latitude;
        }

        public SLocationInfo(float a_altitude, float a_longitude, float a_latitude)
        {
            altitude = a_altitude;
            longitude = a_longitude;
            latitude = a_latitude;
        }

        public string GetInfo()
        {
            return latitude + ", " + longitude + ", " + altitude;
        }
    }

    public static Vector3 GetPositionDiff(SLocationInfo locationRef, SLocationInfo location)
    {
        float latitudeDiff = location.latitude - locationRef.latitude;
        float latitudeDistance = latitudeDiff * Mathf.Deg2Rad * (earthRadius + locationRef.altitude);

        float longitudeRadius = (earthRadius + locationRef.altitude) * Mathf.Sin(Mathf.Abs(locationRef.latitude) * Mathf.Deg2Rad);

        float longitudeDiff = location.longitude - locationRef.longitude;
        float longitudeDistance = longitudeDiff * Mathf.Deg2Rad * longitudeRadius;

        float altitudeDistance = location.altitude - locationRef.altitude;

        return new Vector3(longitudeDistance, altitudeDistance, latitudeDistance);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="locationRef"></param>
    /// <param name="offset">(longitudeDistance, altitudeDistance, latitudeDistance)
    /// (eastDirection, verticalDirection, northDirection)</param>
    /// <returns></returns>
    public static SLocationInfo GetLocalization(SLocationInfo locationRef, Vector3 offset)
    {
        float altitudeDistance = offset.y;
        float altitude = locationRef.altitude + altitudeDistance;

        float latitudeDistance = offset.z;
        float latitudeDiff = latitudeDistance / (Mathf.Deg2Rad * (earthRadius + locationRef.altitude));
        float latitude = latitudeDiff + locationRef.latitude;

        float longitudeRadius = (earthRadius + locationRef.altitude) * Mathf.Sin(Mathf.Abs(locationRef.latitude) * Mathf.Deg2Rad);
        float longitudeDistance = offset.x;
        float longitudeDiff = longitudeDistance / (Mathf.Deg2Rad * longitudeRadius);
        float longitude = longitudeDiff + locationRef.longitude;

        return new SLocationInfo(altitude, longitude, latitude);
    }

    public static Vector3 WorldOffsetToNorthRefOffset(Vector3 worldOffset)
    {
        north.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);

        Vector3 northRefOffset = new Vector3();
        northRefOffset.y = worldOffset.y;
        northRefOffset.z = Vector3.Dot(worldOffset, north.forward);
        northRefOffset.z = Vector3.Dot(worldOffset, north.right);

        return northRefOffset;
    }

    public static Vector3 NorthRefOffsetToWorldOffset(Vector3 northRefOffset)
    {
        north.rotation = Quaternion.Euler(0, Input.compass.trueHeading, 0);

        Vector3 worldOffset = north.forward * northRefOffset.z + north.right * northRefOffset.x + Vector3.up * northRefOffset.y;

        return worldOffset;
    }
}
