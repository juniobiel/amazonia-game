using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionEndAnimation : MonoBehaviour
{
  Text MissionEnd;
  float count = 0;
  string textMissionEnd = "MISSÃO CUMPRIDA!";
  int textIndex = 0;

  void OnEnable() 
  {
    MissionEnd = gameObject.GetComponent<Text>();
    MissionEnd.text = "";
  }
  void Update()
  {
    count += Time.deltaTime;
    if(!MissionEnd.text.Equals(textMissionEnd) && count >= 0.1f)
    {
      if(textIndex < textMissionEnd.Length)
      {
        MissionEnd.text += textMissionEnd[textIndex];
        textIndex++;
        count -= count;
      }
    }
    else if(MissionEnd.text.Equals(textMissionEnd) && count >= 1f)
    {
      count -= count;
      textIndex = 0;
      gameObject.SetActive(false);
    }
  }
}
