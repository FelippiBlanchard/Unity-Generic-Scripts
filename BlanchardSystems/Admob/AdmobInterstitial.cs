#if GoogleMobileAds
    using GoogleMobileAds.Api;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace BlanchardSystems.Admob
{
    public class AdmobInterstitial : MonoBehaviour
    {
#if GoogleMobileAds
    [Header("Interstitial ID")]
    [SerializeField] private StringScriptableVariable interstitialAdUnitIdAndroid;
    [SerializeField] private StringScriptableVariable interstitialAdUnitIdIOS;

    [Header("Control to show")]
    [SerializeField] private int instanceID;
    [SerializeField] private int attemptsToShow;
    [SerializeField] private IntScriptableVariable contAttempts;

    [Header("Events")]
    [SerializeField] private UnityEvent OnShowInterstitial;
    [SerializeField] private UnityEvent OnCloseInterstitial;

    private InterstitialAd interstitial;

    [Header("Custom Debug")]
    [SerializeField] private TextMeshProUGUI textError;
    
    private static List<AdmobInterstitial> instances = new List<AdmobInterstitial>();


    private void OnEnable()
    {
        while (instances.Count < instanceID + 1)
        {
            instances.Add(null);
        }
        if (instances[instanceID] == null)
        {
            DontDestroyOnLoad(gameObject);
            RequestInterstitial();
            instances[instanceID] = this;
        }
        else
        {
            instances[instanceID].OnShowInterstitial = OnShowInterstitial;
            instances[instanceID].OnCloseInterstitial = OnCloseInterstitial;
            if (textError != null)
            {
                textError.text = instances[instanceID].interstitial.IsLoaded().ToString();
            }
        }
    }

    
    #region Interstitial

    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = interstitialAdUnitIdAndroid.value;
#elif UNITY_IPHONE
                string adUnitId = interstitialAdUnitIdIOS.value;
#else
                string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }




    public void ShowInterstitial(float delay = 0)
    {
        StartCoroutine(WaitToShowInterstitial(delay));
    }
    private IEnumerator WaitToShowInterstitial(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        if (instances[instanceID].interstitial.IsLoaded())
        {
            instances[instanceID].interstitial.Show();
        }
    }

    public void TryShowInterstitial(float delay = 0)
    {
        contAttempts.AddValue();
        if(contAttempts.value >= attemptsToShow)
        {
            ShowInterstitial(delay);
            contAttempts.SetValue();
        }
    }


    public void CloseInterstitial(float delay = 0)
    {
        StartCoroutine(WaitToCloseInterstitial(delay));
    }
    private IEnumerator WaitToCloseInterstitial(float delay)
    {
        yield return new WaitForSeconds(delay);
        instances[instanceID].interstitial.Destroy();
        Destroy(instances[instanceID].gameObject);
    }



    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
        if (textError != null)
        {
            textError.text = "ok";
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.LoadAdError.GetMessage());
        if (textError != null)
        {
        textError.text = args.LoadAdError.GetMessage();
        }
    }
    

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        OnShowInterstitial.Invoke();
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        OnCloseInterstitial.Invoke();
        Destroy(instances[instanceID].gameObject);
        instances[instanceID] = null;
    }

    #endregion

#else
        private void Awake()
        {
            Debug.Log("Admob library need to be imported and defined");
        }
#endif
    }
}
