using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
  [Header("GAME MANAGER DIRETÓRIO DE MISSÕES")]
  public GameManager GameManager;

  [Header("BOTÃO DE INTERAÇÃO")]
  public GameObject interactionButton;
  private float widthInteractionButton;
  private float heightInteractionButton;
  private float widthButtonVariation = 90;
  private float heightButtonVariation = 90;

  [Header("CAMPO DE MISSÃO")]
  public GameObject missionMenuUI;
  public Text missionText;
  public Text missionTip;
  public bool showMission = false;

  [Header("DISPLAY DA MISSÃO")]
  public GameObject missionDisplayUI;
  public bool missionDisplay = false;
  public GameObject MissionEndDisplayUI;
  public GameObject endUI;

  [Header("DISPLAY DE OBJETIVOS")]
  public GameObject objectiveDisplayUI;

  [Header("CAMPO DE VIDA")]
  public GameObject[] heartIcons;

  [Header("MENU DE CONFIGURAÇÕES")]
  public static bool GameIsPaused = false;
  public GameObject configMenuUI;
  public GameObject baseUI;
  public GameObject volumeBarUI;
  public bool audioMuted = false;
  public Sprite muteOn;
  public Sprite muteOff;
  public GameObject muteButtonUI;
  public GameObject seta;

  [Header("BARRA DE VITAMINA/FOME")]
  public GameObject vitalityBarUI;

  // Start is called before the first frame update
  void Start()
  {

    GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    missionMenuUI.SetActive(false);

    // Tamanho padrão do botão de interação
    widthInteractionButton = interactionButton.GetComponent<RectTransform>().rect.width;

    heightInteractionButton = interactionButton.GetComponent<RectTransform>().rect.height;

    MissionEndDisplayUI.gameObject.SetActive(false); 

  }

  void Update()
  {
    if(Input.GetKeyDown(KeyCode.Escape))
    {
      if(GameIsPaused)
      {
        Resume();
      }
      else
      {
        ConfigMenu();
      }
    }

    if(Input.GetKeyDown(KeyCode.M))
    {
      if(showMission)
      {
        Resume();
      }
      else
      {
        MissionMenu();
      }
    }

    if(Input.GetKeyDown(KeyCode.E) && GameManager.GetDistanceToNPC() <= 2.5f)
    {
      if(missionDisplay)
      {
        Resume();
      }
      else
      {
        MissionDisplay();
      }
    }
  }  

  // ------------------------ Game Operations ------------------- 

  public void Resume()
  {
    if(GameIsPaused)
    {
      configMenuUI.SetActive(false);
    } 
    else if (showMission)
    {
      missionMenuUI.SetActive(false);
    } 
    else if (missionDisplay)
    {
      missionDisplayUI.SetActive(false);
    } 

    baseUI.SetActive(true);
    Time.timeScale = 1f;
    GameIsPaused = false;
    showMission = false;
    missionDisplay = false;

    if(GameManager.GetCurrentMission() == 0)
    {
      SetObjectiveDisplayON();
    }
  }

  public void MissionDisplay()
  {
    if(GameManager.GetDistanceToNPC() <= 2.5f)
    {
      //Define as ações de encerramento do jogo
      if(GameManager.GetCurrentMission() == -1 && GameManager.GetMissionsCompleted() == 2)
      {
        baseUI.SetActive(false);
        endUI.SetActive(true);
      }
      else
      {
        baseUI.SetActive(false);
        missionDisplayUI.SetActive(true);
        missionDisplay = true;
        SetObjectiveDisplayOFF();
      }
      
    }
    
  }

  public void MissionMenu()
  {
    baseUI.SetActive(false);
    missionMenuUI.SetActive(true);
    SetObjectiveDisplayOFF();

    Time.timeScale = 0f;
    showMission = true;

    if(GameManager.GetCurrentMission() == -1)
    {
      //Caso o índice de missões aponte para o -1, quer dizer que não há missões ativas.
      missionText.text = GameManager.GetMissionIndex(GameManager.GetCurrentMission());
      missionTip.text = "Henrique te aguarda para uma nova aventura!";
    }
    else
    {
      //Caso não, deve-se haver a tratativa da missão ativa, provisioriamente, se mantém a missão inicial sobre o desmatamento.
      missionText.text = GameManager.GetMission(GameManager.GetCurrentMission()).title;
      missionTip.text = GameManager.GetMission(GameManager.GetCurrentMission()).missionTip;
      
    }
    
  }

  public void ConfigMenu()
  {
    baseUI.SetActive(false);
    configMenuUI.SetActive(true);
    SetObjectiveDisplayOFF();

    Time.timeScale = 0f;
    GameIsPaused = true;
  }

  // ------------------------ Configs Operations ------------------- 
  public void ToggleMuteAudio()
  {
      if(audioMuted)
      {
        volumeBarUI.GetComponent<Scrollbar>().value = PlayerPrefs.GetFloat("volume");;
        muteButtonUI.GetComponent<Image>().sprite = muteOff;
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        audioMuted = false;
      }
      else
      {
        volumeBarUI.GetComponent<Scrollbar>().value = 0;
        muteButtonUI.GetComponent<Image>().sprite = muteOn;
        AudioListener.volume = 0;
        audioMuted = true;
      }
      
  }

  public void OnChangeVolumeBar()
  {
      float selectedVolume = volumeBarUI.GetComponent<Scrollbar>().value;
      if(!audioMuted && selectedVolume != 0)
      {
        AudioListener.volume = selectedVolume;
        PlayerPrefs.SetFloat("volume", selectedVolume);
      } else if(selectedVolume == 0) 
      {
        ToggleMuteAudio();
      } 
      else if(audioMuted)
      {
        ToggleMuteAudio();
      }
  }

  // ------------------------ Player Stats Operations ------------------- 

  public void ReduceLife(int currentLife)
  {
    int life = currentLife -1;
    heartIcons[life].gameObject.SetActive(false);
  }


  // ------------------------ Missions Operations ------------------- 
  
  public void OnClickDeclineMission()
  {
    Resume();
  }

  public void OnClickAcceptMission()
  {
    GameManager.MissionStart();
    Resume();
  }

  public void SetObjectiveDisplayON()
  {
    objectiveDisplayUI.gameObject.SetActive(true);
  }

  public void SetObjectiveDisplayOFF()
  {
    objectiveDisplayUI.gameObject.SetActive(false);
  }

  public void MissionEndDisplay()
  {
    MissionEndDisplayUI.gameObject.SetActive(true);
  }

  
  // ------------------------ NPC Operations ------------------- 

  //Botão de interação da missão
  public void ToggleInteractionButton(string status)
  {
    Vector2 sizeNow = interactionButton.GetComponent<RectTransform>().sizeDelta;
    Vector2 sizeNormal = new Vector2(widthInteractionButton, heightInteractionButton);
    Vector2 sizeVariation = new Vector2(widthButtonVariation, heightButtonVariation);

    if(status == "active")
    {
      if(sizeNow == sizeNormal)
      {
        interactionButton.GetComponent<RectTransform>().sizeDelta = sizeVariation;
      }
      else if(sizeNow == sizeVariation)
      {
        interactionButton.GetComponent<RectTransform>().sizeDelta = sizeNormal;
      }
    }
    else
    {
      interactionButton.GetComponent<RectTransform>().sizeDelta = sizeNormal;
    }
    
  }
    
}
