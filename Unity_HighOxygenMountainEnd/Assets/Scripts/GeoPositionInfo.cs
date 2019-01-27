using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeoPositionInfo : MonoBehaviour
{
    public Text northRefOffsetInfo;
    public Text calculatedLocalizationInfo;

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
        GetPlayerPosition();
        if (playerPosition)
        {
            Vector3 northRefOffset = GPSPositionUtils.GetPositionDiff(playerPosition.gpsPosition.lastData,
                calculatedLocalization);
            Vector3 offset = GPSPositionUtils.NorthRefOffsetToWorldOffset(northRefOffset);

            transform.position = offset;
        } 
    }

    void CalculateLocalization()
    {
        GetPlayerPosition();
        if (playerPosition)
        {
            Vector3 northRefOffset = GPSPositionUtils.WorldOffsetToNorthRefOffset(transform.position - playerPosition.transform.position);
            calculatedLocalization = GPSPositionUtils.GetLocalization(playerPosition.gpsPosition.lastData, northRefOffset);
        }
    }
}