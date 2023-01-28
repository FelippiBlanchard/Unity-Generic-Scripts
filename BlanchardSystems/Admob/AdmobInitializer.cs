#if GoogleMobileAds
        using GoogleMobileAds.Api;
#endif

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlanchardSystems.Admob
{
    public class AdmobInitializer : MonoBehaviour
    {
#if GoogleMobileAds
    private void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
#else
        private void Awake()
        {
            Debug.Log("Admob library need to be imported and defined");
        }
#endif
    }
}