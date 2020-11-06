using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  Dictionary<int, Mission> missionsDirectory;


  [Header("GUI MANAGER")]
  public GUIManager guiManager;

  [Header("MISSÕES")] 

  public Mission Desmatamento;
  private int currentMission = -1;

  private bool initialMission = true;

  [Header("NPCS")]
  public GameObject henriqueNPC;

  [Header("PLAYER")]
  public GameObject titi;

  private float count = 0;
  private float distanceToNPC = 0;
  
  // Start is called before the first frame update
  void Start()
  {
    guiManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
    missionsDirectory = new Dictionary<int, Mission>();

    missionsDirectory.Add(0, Desmatamento);

    Debug.Log(currentMission);
  }

  private void LateUpdate() 
  {
    distanceToNPC = Vector3.Distance(henriqueNPC.GetComponent<Transform>().position, titi.GetComponent<Transform>().position);

    if(distanceToNPC <= 2.5f)
    {
      count += Time.deltaTime;
      if(count >= 1.5f)
      {
        count = 0;
        guiManager.ToggleInteractionButton("active");
      }
    } 
    else
    {
      guiManager.ToggleInteractionButton("inactive");
    }
  }

  // ------------------------ alteração de cenas ------------------

  public void OnBtnJogar()
  {
    SceneManager.LoadScene("Amazonia", LoadSceneMode.Single);
  }

  public void OnBtnMenu()
  {
    SceneManager.LoadScene("Menu", LoadSceneMode.Single);
  }

  // ------------------------ Getters e Setters ------------------- 

  public Mission GetMission(int id)
  {
    return missionsDirectory[id];
  }

  public void MissionStart()
  {
    if(initialMission)
    {
      currentMission = 0;
      initialMission = false;
    }
    else
    {
      currentMission += 1;
    }
    Debug.Log(currentMission);
  }

  public int GetCurrentMission()
  {
    return currentMission;
  }

  public float GetDistanceToNPC()
  {
    return distanceToNPC;
  }
}
