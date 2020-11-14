using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    MonetizationManager monetizationManager;
    
    public Button insterstitialButton;
    public Button rewardedButton;
    public Text coinsText;

    // Start is called before the first frame update
    void Start()
    {
        monetizationManager = FindObjectOfType<MonetizationManager>();
    }

    private void Update() 
    {  
        insterstitialButton.interactable = monetizationManager.InterstitialIsReady();
        rewardedButton.interactable = monetizationManager.RewardedIsReady();
        coinsText.text = "COINS: " + PlayerPrefs.GetInt("COINS").ToString("N0");
    }

    //--------------------------------------------------
    public void OnInterstitialButtonClick()
    {
        //Arquivo que faz a invocação do Ad.
        monetizationManager.ShowInterstitial();
    }

    //--------------------------------------------------
    public void OnRewardedButtonClick()
    {
        //Arquivo que faz a invocação do Ad.
        monetizationManager.ShowRewarded();
    }

    //--------------------------------------------------

}
