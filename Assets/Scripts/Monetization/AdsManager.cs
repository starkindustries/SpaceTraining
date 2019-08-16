using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager: MonoBehaviour
{
#if UNITY_ANDROID
    private const string gameId = "3257633";
#endif

#if UNITY_IOS
    private const string gameId = "3257632";
#endif

    public float timeBetweenAds = 60 * 5;

    private float timeSinceLastAd = 0;

    // Singleton pattern
    // https://gamedev.stackexchange.com/a/116010/123894
    private static AdsManager _instance;
    public static AdsManager Instance { get { return _instance; } }

    private void Awake()
    {
        // Singleton Enforcement Code
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            _instance = this;
        }        
    }

    private void Start()
    {
        Advertisement.Initialize(gameId: gameId, testMode: true);
    }

    private void Update()
    {
        timeSinceLastAd += Time.deltaTime;
    }    

    public static void ShowVideoAdWhenReady()
    {
        const string placementId = "video";

        if (Instance.timeSinceLastAd < Instance.timeBetweenAds)
        {
            return;
        }

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
