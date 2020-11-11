using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  //define um índice para os objetos de missões (Scriptable Objects)
  Dictionary<int, Mission> missionsDirectory;
  //define um índice para o sistema de missões (Status das missões.)
  Dictionary<float, string> indexOfMissions;


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
    indexOfMissions = new Dictionary<float, string>();

    /* 
    Para fins de consultas e organização, cada índice irá indicar qual é o status da missão.
    Se a variável currentMission se encontra com valor -1, então sabe-se que não existe missão atribuída/ativa em execução
    Se a variável possui valor 0, então a missão atual é a de Desmatamento.
    Como o sistema de missões não permite adquirir mais de uma missão por vez, o índice serve apenas para
    orientação.
    */
    indexOfMissions.Add(-1f, "Nenhuma missão atribuída");
    indexOfMissions.Add(0f, "Missão inicial sobre o desmatamento");

    /*
      Aqui se faz o diretório dos objetos de Missão, que contém Nome, descrição e todas as propriedades. É utilizada
      para fazer o índice dos ScriptablesObjects.
    */
    missionsDirectory.Add(0, Desmatamento);
    
  }

  private void LateUpdate() 
  {
    distanceToNPC = Vector3.Distance(henriqueNPC.GetComponent<Transform>().position, titi.GetComponent<Transform>().position);

    if(distanceToNPC <= 2.5f)
    {
      //Se o jogador estiver perto para interagir com o NPC, deve-se ativar o botão.
      guiManager.interactionButton.SetActive(true);
      count += Time.deltaTime;
      if(count >= 1.5f)
      {
        count = 0;
        guiManager.ToggleInteractionButton("active");
      }
    } 
    else
    {
      //Se ele ficar longe do NPC, deve-se desativar o botão de interação.
      guiManager.interactionButton.SetActive(false);
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

   public string GetMissionIndex(int id)
  {
    return indexOfMissions[id];
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
