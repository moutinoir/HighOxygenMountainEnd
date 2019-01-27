using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeoPositionInfo : MonoBehaviour
{
    public TextMesh northRefOffsetInfo;
    public TextMesh calculatedLocalizationInfo;

    private Vector3 northRefOffset;
    public GPSPositionUtils.SLocationInfo calculatedLocalization;
    public bool useCalculatedLocalization;
    private bool isInitialized;

    private PlayerPosition playerPosition;

    //private void Start()
    //{
    //    StartCoroutine(Initialize());
    //}

    public IEnumerator Initialize()
    {
        GetPlayerPosition();
        PlayerPosition.debug.text += "initialize " + gameObject.name + "\n";
        while(!isInitialized)
        {
            if (playerPosition && playerPosition.gpsPosition.hasLastData)
            {
                if (useCalculatedLocalization)
                {
                    SetPosition();
                    UpdateInfo();
                    isInitialized = true;
                }
                else
                {
                    CalculateLocalization();
                    UpdateInfo();
                    isInitialized = true;
                }
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
            }
        }
    }

    public void GetPlayerPosition()
    {
        playerPosition = FindObjectOfType<PlayerPosition>();
    }

    void UpdateInfo()
    {
        GetPlayerPosition();
        if (playerPosition)
        {
            northRefOffset = GPSPositionUtils.WorldOffsetToNorthRefOffset(transform.position - playerPosition.transform.position);
            northRefOffsetInfo.text = northRefOffset + "";

            calculatedLocalization = GPSPositionUtils.GetLocalization(playerPosition.gpsPosition.lastData, northRefOffset);
            calculatedLocalizationInfo.text = calculatedLocalization.GetInfo();
        }
    }

    void SetPosition()
    {
        PlayerPosition.debug.text += "set position \n";
        GetPlayerPosition();
        if (playerPosition)
        {
            Vector3 northRefOffset = GPSPositionUtils.GetPositionDiff(playerPosition.gpsPosition.lastData,
                calculatedLocalization);
            Vector3 offset = GPSPositionUtils.NorthRefOffsetToWorldOffset(northRefOffset);
            PlayerPosition.debug.text += "current position " + transform.position + "\n";
            transform.position = offset;
            PlayerPosition.debug.text += "current position " + transform.position + "\n";
        } 
    }

    void CalculateLocalization()
    {
        PlayerPosition.debug.text += "calculate localization \n";
        GetPlayerPosition();
        if (playerPosition)
        {
            Vector3 northRefOffset = GPSPositionUtils.WorldOffsetToNorthRefOffset(transform.position - playerPosition.transform.position);
            PlayerPosition.debug.text += "northRefOffset " + northRefOffset + "\n";
            calculatedLocalization = GPSPositionUtils.GetLocalization(playerPosition.gpsPosition.lastData, northRefOffset);
            PlayerPosition.debug.text += "calculatedLocalization " + calculatedLocalization.GetInfo() + "\n";
        }
    }

    //private void Update()
    //{
    //    if (useCalculatedLocalization)
    //    {
    //        SetPosition();
    //        UpdateInfo();
    //    }
    //    else
    //    {
    //        CalculateLocalization();
    //        UpdateInfo();
    //    }
    //}
}