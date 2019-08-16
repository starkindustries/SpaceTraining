using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager
{
    public static void ShowRewardedAd(ShowOptions showOptions)
    {
        Debug.Log("Showing rewarded ad");
        const string placementId = "rewardedVideo";

        if (Advertisement.IsReady(placementId: placementId))
        {
            Advertisement.Show(placementId: placementId, showOptions: showOptions);
        }
    }

    public static void ShowVideoAd()
    {
        const string placementId = "video";

        if (Advertisement.IsReady(placementId: placementId))
        {
            Debug.Log("Showing video ad.");
            Advertisement.Show(placementId: placementId);
        }
        else
        {
            Debug.Log("Video ad not ready.");
        }
    }
}
