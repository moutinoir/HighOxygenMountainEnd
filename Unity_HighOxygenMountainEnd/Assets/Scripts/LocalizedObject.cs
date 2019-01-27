using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizedObject : MonoBehaviour
{
    public GeoPositionInfo geoPositionInfo;
    private HiddenObjectsManager manager;

    private void Start()
    {
        manager = FindObjectOfType<HiddenObjectsManager>();
        HiddenObjectsManager.SLocalizedObject localizedObjectInfo = manager.GetObject();
        if(localizedObjectInfo.name != null)
        {
            geoPositionInfo.calculatedLocalization.altitude = localizedObjectInfo.localization.altitude;
            geoPositionInfo.calculatedLocalization.latitude = localizedObjectInfo.localization.latitude;
            geoPositionInfo.calculatedLocalization.longitude = localizedObjectInfo.localization.longitude;
            geoPositionInfo.useCalculatedLocalization = true;
            geoPositionInfo.name = localizedObjectInfo.name;
            PlayerPosition.debug.text += "create object " + localizedObjectInfo.name;
        }
        else
        {
            geoPositionInfo.name = "Ground Plane Stage";
        }
        StartCoroutine(geoPositionInfo.Initialize());
    }
}
