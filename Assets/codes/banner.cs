using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
public class banner : MonoBehaviour
{

    private BannerView reklamObjesi;
   
    void Start()
    {

        MobileAds.Initialize(reklamDurumu => { });

        reklamObjesi = new BannerView("ca-app-pub-1050058637224524/7433815152", AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest reklamIstegi = new AdRequest.Builder().Build();
        reklamObjesi.LoadAd(reklamIstegi);
       
    }

    void OnDestroy()
    {
        if (reklamObjesi != null)
            reklamObjesi.Destroy();
    }


}
