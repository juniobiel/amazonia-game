using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Mission")]
public class Mission : ScriptableObject
{
  [Header("DADOS DA MISSÃO")]
  public string title;

  public string description;
  
  public string missionTip;

  [Header("DADOS DO NPC")]
  public string NPCName;
  
  public Sprite NPCImage;
}
