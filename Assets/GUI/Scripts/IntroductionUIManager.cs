using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionUIManager : MonoBehaviour
{

  [Header("ELEMENTOS PARA A NARRATIVA")]
  public GameObject NarrativeUI;
  public Image npcIconUI;
  public Text npcNameTextUI;
  public Image npcNarrativeFieldUI;
  public Text narrativeTextUI;
  public List<string> scriptNarrativeText;
  public GameObject IntroductionCamera;

  [Header("ELEMENTOS PARA INTERAÇÃO COM O PLAYER!")]
  public GameObject CollectPlayerNameUI;
  public InputField InputNameFieldUI;
  public Text WarningText;


  [Header("ELEMENTOS DA GAMEPLAY PARA START")]
  public GameObject Player;
  //public GameObject CameraS;
  //public GameObject Arrow;
  public GameObject Enemy;
  public GameObject GUI;
  public GameObject GameManager;

  string npcName;

  //Controladores de estados da Interface
  bool showNPCIcon;
  bool showNPCNameText;
  bool showNarrativeFieldUI;
  bool showNarrativeTextUI;
  bool showPlayerPopUp;
  bool nameCollected;
  bool nextScript;


  //Indexadores
  float count;
  int textIndex;
  public int narrativeIndex;
  //Define o final da narrativa de introdução
  public bool introductionEnd = false;

  void Start()
  {
    nextScript = false;
    showNPCIcon = false;
    npcIconUI.fillAmount = 0;

    showNarrativeFieldUI = false;
    npcNarrativeFieldUI.fillAmount = 0;

    showNarrativeTextUI = false;
    showNPCNameText = false;
    
    npcName =  "Henrique";
    npcNameTextUI.text = "";

    

    scriptNarrativeText = new List<string>();
    narrativeTextUI.text = "";
    narrativeIndex = 0;
    scriptNarrativeText.Add("Olá! Como é bom ver você por aqui!\nMe chamo Henrique e irei te guiar nesta aventura...\nComo posso te chamar?");
    showPlayerPopUp = false;

    count = 0;
    textIndex = 0;

    NarrativeUI.SetActive(true);
    CollectPlayerNameUI.SetActive(false);

    nameCollected = false;
  }

  void FixedUpdate() 
  {
    count += Time.deltaTime;

    //Parte 1, apresentação e coleta de nome
    if(!showNPCIcon && count >= 0.02f)
      AnimateShowNPCIcon();
    else if (showNPCIcon && !showNarrativeFieldUI && count >= 0.02f)
      AnimateShowNarrativeField();
    else if (showNarrativeFieldUI && !showNPCNameText && count >= 0.1f)
      AnimateShowNPCName();
    else if (showNPCNameText && !showNarrativeTextUI && count >= 0.05f)
      AnimateShowNarrativeText(narrativeIndex);
    else if (showNarrativeTextUI && !showPlayerPopUp && !nameCollected)
      Invoke("ShowCollectNamePopUp", 1f);
        

    //Parte 2
    if(nameCollected && narrativeIndex == 1 && count >= 0.05f)
    {
      AnimateShowNarrativeText(narrativeIndex);
    }
    else if (narrativeIndex == 2 && count >= 0.05f && nextScript)
    {
      AnimateShowNarrativeText(narrativeIndex);
    }
    else if (narrativeIndex == 3 && count >= 0.05f && nextScript)
    {
      AnimateShowNarrativeText(narrativeIndex);
    }
    else if(introductionEnd)
    {
      StartGameNow();
    }
  }

  //Função recursiva no método Update para animação do ícone do NPC
  void AnimateShowNPCIcon()
  {
    if(npcIconUI.fillAmount < 1)
    {
      npcIconUI.fillAmount += 0.05f;
      count -= count;
    }
    else if (npcIconUI.fillAmount >= 1)
    {
      showNPCIcon = true;
      count -= count;
    }   
  }

  //Função recursiva no método Update para animação do campo de inserção de texto
  void AnimateShowNarrativeField()
  {
    if(npcNarrativeFieldUI.fillAmount < 1)
    {
      npcNarrativeFieldUI.fillAmount += 0.03f;
      count -= count;
    }
    else if(npcNarrativeFieldUI.fillAmount >= 1)
    {
      showNarrativeFieldUI = true;
      count -= count;
    }
  }

  //Função recursiva no método Update para animação do nome do NPC
  void AnimateShowNPCName()
  {
    npcNameTextUI.gameObject.SetActive(true);

    if(!npcNameTextUI.text.Equals(npcName))
    {
      npcNameTextUI.text += npcName[textIndex];
      textIndex++;
      count -= count;
    }
    else
    {
      textIndex = 0;
      count -= count;
      showNPCNameText = true;
    }
  }

  //Função recursiva no método Update para animação da Narrativa
  void AnimateShowNarrativeText(int id)
  {
    narrativeTextUI.gameObject.SetActive(true);
    if(!narrativeTextUI.text.Equals(scriptNarrativeText[id]))
    {
      narrativeTextUI.text += scriptNarrativeText[id][textIndex];
      textIndex++;
      count -= count;
    }
    else
    {
      nextScript = false;
      textIndex = 0;
      count -= count;
      showNarrativeTextUI = true;
      StartCoroutine("SetNarrativeTextNull");
      narrativeIndex++;
    }
  }

  //Reseta o texto para nova inserção
  IEnumerator SetNarrativeTextNull()
  {
    yield return new WaitForSeconds(2f);
    if(narrativeIndex >= 4)
      introductionEnd = true;
    narrativeTextUI.text = "";
    nextScript = true;
    
  }

  //Função chamada de tela para coletar o nome do jogador
  void ShowCollectNamePopUp()
  {
    NarrativeUI.SetActive(false);
    CollectPlayerNameUI.SetActive(true);
    showPlayerPopUp = true;
  }

  //Função do Botão Pronto
  public void OnButtonReady()
  {
    if(InputNameFieldUI.text.Equals("") || InputNameFieldUI.text.Length <= 2)
    {
      WarningText.gameObject.SetActive(true);
    }
    else
    {
      PlayerPrefs.SetString("PlayerName", InputNameFieldUI.text);
      CollectPlayerNameUI.SetActive(false);
      NarrativeUI.SetActive(true);
      showPlayerPopUp = false;
      nameCollected = true;
      PopulateScriptNarrative();      
    }

  }

  /*  Popular a Lista com as falas narrativas em sequência linear de acontecimentos
      1 - (No método Start) Apresentação do NPC
      2 - Identificação do Jogador e introdução sobre Bioma
      3 - Conceituando o Bioma
      4 - Chamada para o início do jogo!
  */
  void PopulateScriptNarrative()
  {
    scriptNarrativeText.Add(PlayerPrefs.GetString("PlayerName") + ", estamos na Amazônia.\nVocê sabia que aqui é um dos maiores biomas brasileiros?\nEspera um pouco, você sabe o que é um Bioma?");
    scriptNarrativeText.Add("Um bioma é onde vivem um monte de animais, plantas, árvores e insetos na natureza!\nÉ importantíssimo para a vida no nosso Planeta!\nAqui na Amazônia existe essa imensa diversidade!");   
    scriptNarrativeText.Add("Aproveitando que está aqui, que tal conhecer um pouco sobre a Amazônia?\nTome cuidado! A selva é perigosa e você provavelmente vai econtrar alguns desafios...\nFique atento!");   
  }

  void StartGameNow()
  {
    Destroy(IntroductionCamera.gameObject);
    gameObject.SetActive(false);

    //Player.GetComponent<Player>().enabled = true;
    //CameraS.GetComponent<Camera>().enabled = true;
    //Arrow.GetComponent<MeshRenderer>().enabled = true;
    Player.gameObject.SetActive(true);
    GUI.gameObject.SetActive(true);
    GameManager.gameObject.SetActive(true);
    Enemy.gameObject.SetActive(true);

    Destroy(gameObject);
  }

}
