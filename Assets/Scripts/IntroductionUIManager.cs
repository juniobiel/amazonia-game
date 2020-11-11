using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroductionUIManager : MonoBehaviour
{

  public Image npcIconUI;
  public Text npcNameTextUI;
  public Image npcNarrativeFieldUI;
  public Text narrativeTextUI;

  public List<string> scriptNarrativeText;

  bool showNPCIcon;
  bool showNPCNameText;
  string npcName;
  bool showNarrativeFieldUI;
  bool showNarrativeTextUI;
  float count;

  int textIndex;
  void Start()
  {
    showNPCIcon = false;
    npcIconUI.fillAmount = 0;

    showNarrativeFieldUI = false;
    npcNarrativeFieldUI.fillAmount = 0;

    showNarrativeTextUI = false;
    showNPCNameText = false;
    
    npcName =  "Henrique";
    npcNameTextUI.text = "";

    narrativeTextUI.text = "";

    scriptNarrativeText = new List<string>();
    scriptNarrativeText.Add("Olá! Como é bom ver você por aqui!\nMe chamo Henrique e irei te guiar nesta aventura...\nComo posso te chamar?");

    count = 0;
    textIndex = 0;
  }

  void FixedUpdate() 
  {
    count += Time.deltaTime;

    if(!showNPCIcon && count >= 0.02f)
      AnimateShowNPCIcon();
    else if (showNPCIcon && !showNarrativeFieldUI && count >= 0.02f)
      AnimateShowNarrativeField();
    else if (showNarrativeFieldUI && !showNPCNameText && count >= 0.1f)
      AnimateShowNPCName();
    else if (showNPCNameText && !showNarrativeTextUI && count >= 0.15f)
      AnimateShowNarrativaText();

  }

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

  void AnimateShowNarrativaText()
  {
    narrativeTextUI.gameObject.SetActive(true);
    if(!narrativeTextUI.text.Equals(scriptNarrativeText[0]))
    {
      narrativeTextUI.text += scriptNarrativeText[0][textIndex];
      textIndex++;
      count -= count;
    }
    else
    {
      textIndex = 0;
      count -= count;
      showNarrativeTextUI = true;
    }
  }

}
