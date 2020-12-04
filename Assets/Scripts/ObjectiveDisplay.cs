using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectiveDisplay : MonoBehaviour
{
  public GameManager GameManager;

  public Text ObjectiveTitle;

  public Text ObjectiveCounting;

  public Slider objectiveProgressSlider;


  void Start()
  {
    GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    objectiveProgressSlider.value = 0;
  }
  
  void Update() 
  {  
    if(GameManager.GetCurrentMission() == 0)
    {
      SetObjectiveData();
    }
    
  }

  

  void SetObjectiveData()
  {
    ObjectiveTitle.text = "Lenhadores atraídos para fora";
    
    ObjectiveCounting.text = GameManager.GetNPCsDestroyed().ToString() + "/5";

    objectiveProgressSlider.value = GameManager.GetNPCsDestroyed();
  }

}
