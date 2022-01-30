using System.Collections;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialVideoControl : MonoBehaviour
{
    /*
    
    Test Reklamaları için kullanılacak kimlikler için link:
    https://developers.google.com/admob/unity/test-ads#sample_ad_units

    */

    public static InterstitialVideoControl instance = null;

    private InterstitialAd interstitial;
    public int adShowDuration = 5;

    bool isAdLoaded = false;
    bool isReadyToShow = false;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        StartCoroutine(StartLoading());
        StartCoroutine(AdShowControl());
    }

    public void Reset()
    {
        isAdLoaded = false;
        isReadyToShow = false;
        StopAllCoroutines();
        StartCoroutine(StartLoading());
        StartCoroutine(AdShowControl());
    }

    void LoadInterstitialAd()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-1050058637224524/4924407168";//TEST
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/8691691433";//TEST
#else
        string adUnitId = "unexpected_platform";
#endif

        // Clean up interstitial ad before creating a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
        }

        // Create an interstitial.
        this.interstitial = new InterstitialAd(adUnitId);

        // Register for ad events.
        this.interstitial.OnAdLoaded += this.HandleInterstitialLoaded;
        this.interstitial.OnAdFailedToLoad += this.HandleInterstitialFailedToLoad;
        this.interstitial.OnAdOpening += this.HandleInterstitialOpened;
        this.interstitial.OnAdClosed += this.HandleInterstitialClosed;
        this.interstitial.OnAdLeavingApplication += this.HandleInterstitialLeftApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        interstitial.LoadAd(request);
    }

    public void ShowInterstitialVideo()
    {
        if (isAdLoaded == false || isReadyToShow == false)
            return;

        if (this.interstitial.IsLoaded())
        {
            Debug.Log("Reklam gösteriliyor..");
            this.interstitial.Show();
        }
        else
        {
            Debug.Log("Interstitial video ad is not ready yet");
        }
    }

    public void HandleInterstitialLoaded(object sender, EventArgs args)
    {
        isAdLoaded = true;
        Debug.Log("HandleInterstitialLoaded event received");
    }

    public void HandleInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log(
            "HandleInterstitialFailedToLoad event received with message: " + args.Message);
    }

    public void HandleInterstitialOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialOpened event received");
    }

    public void HandleInterstitialClosed(object sender, EventArgs args)
    {
        isAdLoaded = false;
        isReadyToShow = false;
        StopAllCoroutines();
        StartCoroutine(StartLoading());
        StartCoroutine(AdShowControl());
        Debug.Log("HandleInterstitialClosed event received");
    }

    public void HandleInterstitialLeftApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleInterstitialLeftApplication event received");
    }

    IEnumerator AdShowControl()
    {
        int elapsedTime = 0;
        while (true)
        {
            yield return new WaitForSeconds(1);

            elapsedTime++;
            if (elapsedTime >= adShowDuration)
            {
                isReadyToShow = true;
                break;
            }
        }
    }

    IEnumerator StartLoading()
    {
        WaitForSeconds duration = new WaitForSeconds(5f);

        while (true)
        {
            if (isAdLoaded)
                break;

            LoadInterstitialAd();

            yield return duration;

        }
    }

}
