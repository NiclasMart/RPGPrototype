using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "RPGPrototype/PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
  public int killAmount;

  public void Reset()
  {
    killAmount = 0;
  }
}
