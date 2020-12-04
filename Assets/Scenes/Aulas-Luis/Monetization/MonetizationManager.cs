using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Purchasing;

public class MonetizationManager : MonoBehaviour, IUnityAdsListener
{

    public bool interstitialIsReady;
    public bool rewardedIsReady;

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
        interstitialIsReady = Advertisement.IsReady("interstitial");
        return interstitialIsReady;
    }

    //--------------------------------------------------
    public void ShowRewarded()
    {
        Advertisement.Show("rewarded");
    }

     public bool RewardedIsReady()
     {
        rewardedIsReady = Advertisement.IsReady("rewarded");
        return rewardedIsReady;
     }

    //--------------------------------------------------
    public void OnPurchaseComplete(Product product)
    {
        if (product.definition.id.Equals("remocaodeanuncios"))
        {
            PlayerPrefs.SetInt("COMPROU_A_REMOCAO_DE_ANUNCIOS", 1);
            PlayerPrefs.Save();
        }
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == "interstitial")
        {
            InterstitialIsReady();
        }

        if (placementId == "rewarded")
        {
            RewardedIsReady();
        }
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
