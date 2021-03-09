using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplay : MonoBehaviour
{
  public Mission mission;

  [Header("ELEMENTOS DA INTERFACE SOBRE A MISSÃO")]
  public Text missionTitle;
  public Text missionDescription;
  public Button acceptButton;
  public Button declineButton;
  public Button resumeButton;

  [Header("ELEMENTOS DA INTERFACE SOBRE O NPC")]
  public Image npcImage;
  public Text npcName;
  int currentMission;

  private void OnEnable() 
  {
    GameManager GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    
    if(GameManager.GetCurrentMission() == -1 && GameManager.GetMissionsCompleted() == 0)
    {
      mission = GameManager.GetMission(0);
      SetCurrentMissionInformation(mission);
      SetDisplayOn();
    }
    else if(GameManager.GetCurrentMission() == -1 && GameManager.GetMissionsCompleted() == 1)
    {
      mission = GameManager.GetMission(1);
      SetCurrentMissionInformation(mission);
      SetDisplayOn();
    }
    else if(GameManager.GetCurrentMission() == 0 || GameManager.GetCurrentMission() == 1 )
    {
      SetDisplayOff();
      mission = GameManager.GetMission(GameManager.GetCurrentMission());
      MissionActiveWarning(mission);
      
    }
    
  }
  
  void SetDisplayOff()
  {
    missionTitle.gameObject.SetActive(false);

    missionDescription.gameObject.SetActive(false);
    
    npcName.gameObject.SetActive(false);
    npcImage.gameObject.SetActive(false);

    acceptButton.gameObject.SetActive(false);
    declineButton.gameObject.SetActive(false);

    resumeButton.gameObject.SetActive(false);
  }
  void SetDisplayOn()
  {
    missionTitle.gameObject.SetActive(true);

    missionDescription.gameObject.SetActive(true);
    
    npcName.gameObject.SetActive(true);
    npcImage.gameObject.SetActive(true);

    acceptButton.gameObject.SetActive(true);
    declineButton.gameObject.SetActive(true);
  }

  public void SetCurrentMissionInformation(Mission currentMissionScriptableObject)
  {
    mission = currentMissionScriptableObject;

    missionTitle.text = mission.title;
    missionDescription.text = mission.description;  
    
    npcName.text = mission.NPCName;
    npcImage.sprite = mission.NPCImage;
  }

  void MissionActiveWarning(Mission currentMisisonScriptableObject)
  {
    mission = currentMisisonScriptableObject;
    missionTitle.text = mission.title;
    missionTitle.gameObject.SetActive(true);

    npcName.gameObject.SetActive(true);
    npcImage.gameObject.SetActive(true);

    missionDescription.text =  currentMisisonScriptableObject.missionTip;
    missionDescription.gameObject.SetActive(true);

    resumeButton.gameObject.SetActive(true);
  }
}
