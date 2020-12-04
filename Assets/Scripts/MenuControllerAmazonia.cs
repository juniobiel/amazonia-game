using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class MenuControllerAmazonia : MonoBehaviour
{
    public GameObject botaoRemoverAds;
    public bool comprouRemovedorDeAds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        comprouRemovedorDeAds = PlayerPrefs.GetInt("COMPROU_A_REMOCAO_DE_ANUNCIOS") == 1;

        botaoRemoverAds.SetActive(!comprouRemovedorDeAds);
    }
    
}
