using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndUI : MonoBehaviour
{
    public GameObject NPCNameText;
    public GameObject EndText;
    private void OnEnable() 
    {
        Invoke("SetNPCNameEnable", 0.2f);
    }

    private void Update()
    {
        if(NPCNameText.activeSelf == true)
        {
            EndText.SetActive(true);
        }
    }
    void SetNPCNameEnable()
    {
        NPCNameText.SetActive(true);
    }
}
