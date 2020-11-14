using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class MonetizationManager : MonoBehaviour, IUnityAdsListener
{
    void Start()
    {
        DontDestroyOnLoad(this);

        string gameId = "";

    #if UNITY_ANDROID
        gameId = "3871811";
    #elif UNITY_IOS
        gameId = "3871810";
    #endif

        Advertisement.AddListener(this);
        //Em modo de desenvolvimento é necessário utilizar o Debug.isDebuigBuild.
        Advertisement.Initialize(gameId, Debug.isDebugBuild);

        
    }

//--------------------------------------------------
    public void ShowInterstitial()
    {
        Advertisement.Show("interstitial");
    }

     public bool InterstitialIsReady()
    {
        return Advertisement.IsReady("interstitial");
    }

    //--------------------------------------------------
    public void ShowRewarded()
    {
        Advertisement.Show("rewarded");
    }

     public bool RewardedIsReady()
    {
        return Advertisement.IsReady("rewarded");
    }

    //--------------------------------------------------


    public void OnUnityAdsReady(string placementId)
    {
        //Executa quando um placementId está pronto para mostrar na tela
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {

        Debug.LogError("UNITY ADS ERROR: " + message);
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //Executa quando um vídeo começa a ser mostrado na tela
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //Quando um placement acabar.
        //throw new System.NotImplementedException();

        if(placementId == "rewarded" && showResult == ShowResult.Finished)
        {
            int coins = PlayerPrefs.GetInt("COINS");
            coins += 5;
            PlayerPrefs.SetInt("COINS", coins);
            PlayerPrefs.Save();
        }
    }
}
