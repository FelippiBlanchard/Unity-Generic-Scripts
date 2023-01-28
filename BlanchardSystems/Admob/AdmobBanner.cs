#if GoogleMobileAds
        using GoogleMobileAds.Api;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.Events;


namespace BlanchardSystems.Admob
{
    public class AdmobBanner : MonoBehaviour
    {
#if GoogleMobileAds
    [Header("Banner ID")]
    [SerializeField] private StringScriptableVariable bannerAdUnitIdAndroid;
    [SerializeField] private StringScriptableVariable bannerAdUnitIdIOS;

    [SerializeField] private Settings bannerType;

    [Header("Control to show")]
    [SerializeField] private int instanceID;
    [SerializeField] private int attemptsToShow;
    [SerializeField] private IntScriptableVariable contAttempts;
    
    private BannerView bannerView;

    [Header("Custom Debug")]
    [SerializeField] private TextMeshProUGUI textError;
    
    private static List<AdmobBanner> instances = new List<AdmobBanner>();
    private bool showing;

    private void OnEnable()
    {

        while (instances.Count < instanceID + 1)
        {
            instances.Add(null);
        }

        if (instances[instanceID] == null)
        {
            DontDestroyOnLoad(gameObject);
            instances[instanceID] = this;
            RequestBanner();
        }
        
    }
    
    #region Banner

    private void RequestBanner()
    {
        #if UNITY_ANDROID
                string adUnitId = bannerAdUnitIdAndroid.value;
        #elif UNITY_IPHONE
                        string adUnitId = bannerAdUnitIdIOS.value;
        #else
                        string adUnitId = "unexpected_platform";
        #endif

        var size = bannerType.GetSize();
        AdSize adSize = new AdSize((int)size.x, (int)size.y);
        this.bannerView = new BannerView(adUnitId, adSize, bannerType.position);
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        bannerView.Hide();
    }

    public void TryShowBanner(float delay = 0)
    {
        contAttempts.AddValue();
        if(contAttempts.value >= attemptsToShow)
        {
            ShowBanner(delay);
            contAttempts.SetValue();

        }
    }
    public void ShowBanner(float delay = 0)
    {
        StartCoroutine(WaitToShowBanner(delay));
    }
    private IEnumerator WaitToShowBanner(float delay)
    {
        showing = true;
        yield return new WaitForSeconds(delay);
        if(instances[instanceID] != null) instances[instanceID].bannerView.Show();
        else bannerView.Show();
    }

    public void CloseBanner(float delay = 0)
    {
        if (showing)
        {
            StartCoroutine(WaitToCloseBanner(delay));   
        }
    }
    private IEnumerator WaitToCloseBanner(float delay)
    {
        yield return new WaitForSeconds(delay);
        instances[instanceID].bannerView.Destroy();
        Destroy(instances[instanceID].gameObject);
        instances[instanceID] = null;
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
        MonoBehaviour.print("Banner failed to load: " + args.LoadAdError.GetMessage());
        if (textError != null)
        {
            textError.text = args.LoadAdError.GetMessage();
        }
    }

    #endregion
    
    
    [Serializable]
    public class Settings
    {
        
        public AdPosition position;

        [Tooltip("BANNER = 320x50,\n " +
            "LARGE_BANNER = 320X100,\n" +
            "MEDIUM_RECTANGLE = 300x250,\n " +
            "FULL_BANNER = 468x60,\n" +
            "LEADERBORD = 728x90")]
        [SerializeField] private AdSize size;

        public Vector2 GetSize()
        {
            switch (size)
            {
                case AdSize.BANNER:
                    return new Vector2(320,50);
                case AdSize.LARGE_BANNER:
                    return new Vector2(320, 100);
                case AdSize.MEDIUM_RECTANGLE:
                    return new Vector2(300, 250);
                case AdSize.FULL_BANNER:
                    return new Vector2(468, 60);
                case AdSize.LEADERBOARD:
                    return new Vector2(728, 90);
            }
            return new Vector2(320, 50);
        }

        public enum AdSize
        {
            BANNER,
            LARGE_BANNER,
            MEDIUM_RECTANGLE,
            FULL_BANNER,
            LEADERBOARD,
        }
        
    }
#else
        private void Awake()
        {
            Debug.Log("Admob library need to be imported and defined");
        }

#endif
    }
}
