using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  public class PlayerInfo : MonoBehaviour
  {
    static GameObject player;

    private void Awake()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      if (!player) Debug.LogError("PlayerInfo: Can't find Player!");
    }

    public static GameObject GetPlayer(){
      return player;
    }
  }
}
