using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsControl : MonoBehaviour
{
    public static AdsControl instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void ShowInterstitial()
    {
        InterstitialVideoControl.instance.ShowInterstitialVideo();
    }
}
