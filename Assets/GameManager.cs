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

  MonetizationManager monetizationManager;

  [Header("GUI MANAGER")]
  public GUIManager GUIManager;

  [Header("MISSÕES")] 
  public Mission Desmatamento;

  public Mission Queimadas;

  [Header("NPCS")]
  public GameObject henriqueNPC;

  [Header("PLAYER")]
  public GameObject titi;

  [Header("OBJETOS PARA MISSÃO INICIAL SOBRE DESMATAMENTO")]
  public GameObject AreaDaMissao;
  public GameObject Inimigos;
  public GameObject seta;

  //Controllers
  int NPCLumberjack;
  int npcsDestroyed = 0;
  int missionsCompleted = 0;
  int currentMission = -1;
  bool initialMission = true;
  float count = 0;
  float distanceToNPC = 0;
  Vector3 missionTwoStartPoint;
  Vector3 missionThreeStartPoint;

  public bool missaoCompletaBool = false;
  public float contadorDosAds = 0;
  public bool comprouRemovedorDeAds;

    // Start is called before the first frame update
    void Start()
  {
    monetizationManager = FindObjectOfType<MonetizationManager>();
    GUIManager = GameObject.FindGameObjectWithTag("GUIManager").GetComponent<GUIManager>();
    
    missionsDirectory = new Dictionary<int, Mission>();
    indexOfMissions = new Dictionary<float, string>();

    missionTwoStartPoint = new Vector3(40.31f, 8.05f, 697.8f);
    missionThreeStartPoint = new Vector3(83.96f, 12.57f, 1311.41f);

    /* 
    Para fins de consultas e organização, cada índice irá indicar qual é o status da missão.
    Se a variável currentMission se encontra com valor -1, então sabe-se que não existe missão atribuída/ativa em execução
    Se a variável possui valor 0, então a missão atual é a de Desmatamento.
    Como o sistema de missões não permite adquirir mais de uma missão por vez, o índice serve apenas para
    orientação.
    */
    indexOfMissions.Add(-1f, "Nenhuma missão atribuída");
    indexOfMissions.Add(0f, "Missão inicial sobre o desmatamento"); 
    indexOfMissions.Add(1f, "Missão sobre as Queimadas");
    
    // Índice adicionado mas não consta como missão, apenas para melhor legibilidade.
    indexOfMissions.Add(2f, "Encerramento da gameplay");


    /*
      Aqui se faz o diretório dos objetos de Missão, que contém Nome, descrição e todas as propriedades. É utilizada
      para fazer o índice dos ScriptablesObjects.
    */

    //Informações da missão de desmatamento adicionadas via editor
    missionsDirectory.Add(0, Desmatamento);

    //Informaçãoes da missão de Queimadas.
    //PlayerPrefs.SetString("PlayerName", "Gabriel");
    Queimadas.description = PlayerPrefs.GetString("PlayerName") + 
    ", um pouco adiante alguns homens colocaram fogo na floresta para fazer um pasto." 
    + "\nMuitas árvores morreram e os animais tiveram que sair de onde moravam por conta do fogo, a única coisa que resta são as cinzas."
    + "\nPor enquanto, não podemos fazer muita coisa... Preciso que você chegue até o outro lado da floresta queimada para podermos salvar alguns dos animais feridos!";
    
    missionsDirectory.Add(1, Queimadas);

    /* 
      Deve-se iniciar desativado os objetos da missão inicial para que não haja conflito se o jogador não obter uma missão.
      Já que não tem implementado um sistema de respawn de NPC.
    */

    AreaDaMissao.gameObject.SetActive(false);
    Inimigos.gameObject.SetActive(false);

    /* Para testar a partir da segunda missão apenas
    SetCurrentMission(1);
    gameObject.AddComponent<MonetizationManager>();
    missionsCompleted = 1;
    PlayerPrefs.SetInt("COMPROU_A_REMOCAO_DE_ANUNCIOS", 1); 
    */

    //Para testar a partir da terceira missão apenas
    /*SetCurrentMission(-1);
    gameObject.AddComponent<MonetizationManager>();
    currentMission = 1;
    missionsCompleted = 1;
    PlayerPrefs.SetInt("COMPROU_A_REMOCAO_DE_ANUNCIOS", 1);
    Invoke("MissionEnd", 3f);*/

  }

  private void LateUpdate() 
  {
    distanceToNPC = Vector3.Distance(henriqueNPC.GetComponent<Transform>().position, titi.GetComponent<Transform>().position);

    if(distanceToNPC <= 2.5f)
    {
      //Se o jogador estiver perto para interagir com o NPC, deve-se ativar o botão.
      GUIManager.interactionButton.SetActive(true);
      count += Time.deltaTime;
      if(count >= 1.5f)
      {
        count = 0;
        GUIManager.ToggleInteractionButton("active");
      }
    } 
    else
    {
      //Se ele ficar longe do NPC, deve-se desativar o botão de interação.
      GUIManager.interactionButton.SetActive(false);
      GUIManager.ToggleInteractionButton("inactive");
    }

  }

    private void Update()
    {
        comprouRemovedorDeAds = PlayerPrefs.GetInt("COMPROU_A_REMOCAO_DE_ANUNCIOS") == 1;

        if (missaoCompletaBool && !comprouRemovedorDeAds)
        {
            titi.GetComponent<Rigidbody>().isKinematic = true;
            contadorDosAds += Time.deltaTime;
            if (contadorDosAds > 2.5f)
            {
                monetizationManager.ShowRewarded();
                missaoCompletaBool = false;
                contadorDosAds = 0;
                titi.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    // ------------------------ Gerenciamento de Missões ------------------
    public void MissionStart()
  {
    if(initialMission)
    {
      seta.SetActive(false);
      currentMission = 0;
      initialMission = false;
      
      AreaDaMissao.gameObject.SetActive(true);
      Inimigos.gameObject.SetActive(true);
      SetLumberjackQuantityNumber();
      GUIManager.SetObjectiveDisplayON();
    }
    if(missionsCompleted == 1)
    {
      currentMission = 1;
    }
  }

  public void MissionEnd()
  {
    if(currentMission == 0)
    {
      GUIManager.SetObjectiveDisplayOFF();
      Destroy(AreaDaMissao);
      Destroy(Inimigos);
      henriqueNPC.transform.position = missionTwoStartPoint;
      henriqueNPC.transform.rotation = Quaternion.Euler(0, -120, 0);
      seta.SetActive(true);
    }

    if(currentMission == 1)
    {
      henriqueNPC.transform.position = missionThreeStartPoint;
      henriqueNPC.transform.rotation = Quaternion.Euler(0, 153.257f, 0);
      seta.SetActive(true);
    }
      
    currentMission = -1;
    missionsCompleted++;
    GUIManager.MissionEndDisplay();

    if (!comprouRemovedorDeAds)
    {
      missaoCompletaBool = true;
    }
  }


  // ------------------------ alteração de cenas ------------------

  public void OnBtnJogar()
  {
    SceneManager.LoadScene("Amazonia", LoadSceneMode.Single);
  }

  public void OnBtnMenu()
  {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
  }

    // ------------------------ Getters e Setters ------------------- 

  public int GetMissionsCompleted()
  {
    return missionsCompleted;
  }

  public void SetNPCDestroyed()
  {
    npcsDestroyed++;
    if(npcsDestroyed == 5)
    {
      MissionEnd();
    }
  }

  public int GetNPCsDestroyed()
  {
    return npcsDestroyed;
  }

  public void SetLumberjackQuantityNumber()
  {
    NPCLumberjack = GameObject.FindGameObjectsWithTag("Enemy").Length;
  }

  public int GetLumberjackQuantityNumber()
  {
    return NPCLumberjack;
  }

  public Mission GetMission(int id)
  {
    return missionsDirectory[id];
  }

   public string GetMissionIndex(int id)
  {
    return indexOfMissions[id];
  }

  public int GetCurrentMission()
  {
    return currentMission;
  }

  //Utilizar este método apenas para testes.
  public void SetCurrentMission(int missionId)
  {
    currentMission = missionId;
  }

  public float GetDistanceToNPC()
  {
    return distanceToNPC;
  }
}
