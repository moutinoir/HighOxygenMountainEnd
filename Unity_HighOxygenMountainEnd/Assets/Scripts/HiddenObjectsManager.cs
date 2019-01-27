using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObjectsManager : MonoBehaviour
{
    public SLocalizedObject[] localizedObjects;
    private SLocalizedObject empty = new SLocalizedObject();
    private int currentObject = 0;

    [System.Serializable]
    public struct SLocalizedObject
    {
        public GPSPositionUtils.SLocationInfo localization;
        public string name;
    }

    public SLocalizedObject GetObject()
    {
        if(currentObject < localizedObjects.Length)
        {
            currentObject++;
            return localizedObjects[currentObject - 1];
        }
        return empty;
    }
}
