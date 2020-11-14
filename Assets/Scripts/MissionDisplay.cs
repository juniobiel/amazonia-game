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
  public Text missionReward;
  public Image missionImage;
  public Image missionItem;
  public Image rewardImage;
  public Button acceptButton;
  public Button declineButton;
  public Button resumeButton;

  [Header("ELEMENTOS DA INTERFACE SOBRE O NPC")]
  public Image npcImage;
  public Text npcName;
  int currentMission;

  private void Start() 
  {
    SetDisplayOn();
    SetCurrentMissionInformation(mission);
  }
  private void Update() 
  {
    currentMission = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetCurrentMission();
    Debug.Log(currentMission);
    if(currentMission == -1)
    {
      SetCurrentMissionInformation(mission);
    }
    else if(currentMission == 0)
    {
      mission =  GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().GetMission(currentMission);
      SetDisplayOff();
      MissionActiveWarning(mission);
    }
    else
    {
      SetDisplayOff();
    }
  }

  void SetDisplayOff()
  {
    missionTitle.gameObject.SetActive(false);

    missionDescription.gameObject.SetActive(false);

    missionImage.gameObject.SetActive(false);
    missionItem.gameObject.SetActive(false);

    missionReward.gameObject.SetActive(false);
    rewardImage.gameObject.SetActive(false);
    
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

    missionImage.gameObject.SetActive(true);
    missionItem.gameObject.SetActive(true);

    missionReward.gameObject.SetActive(true);
    rewardImage.gameObject.SetActive(true);
    
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

    missionImage.sprite = mission.image;
    missionItem.sprite = mission.itemCollectable;

    missionReward.text = mission.rewardDescription;
    rewardImage.sprite = mission.rewardImage;   
    
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

    missionDescription.text =  "Vá encontrar o Xaxim e tome cuidado com os lenhadores!";
    missionDescription.gameObject.SetActive(true);

    resumeButton.gameObject.SetActive(true);
  }
}
