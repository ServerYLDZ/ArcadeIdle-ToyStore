using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener

{
    [SerializeField] private string gameID;
    [SerializeField] private bool testMode=true;

    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
     string _adUnitId = null;
   

    // Start is called before the first frame update
      void Awake()
    {
        InitializeAds();
    }

     public void InitializeAds()
    {
    #if UNITY_IOS
            gameID = _iOSGameId;
    #elif UNITY_ANDROID
            gameID = _androidGameId;
    #elif UNITY_EDITOR
            gameID = _androidGameId; //Only for testing the functionality in the Editor
    #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameID, testMode, this);
        }
    }
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }



    
}
