using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  public class PlayerInfo : MonoBehaviour
  {
    static GameObject player;
    static Camera mainCamera;

    private void Awake()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      if (!player) Debug.LogError("PlayerInfo: Can't find Player!");
      mainCamera = Camera.main;
      if (!mainCamera) Debug.LogError("PlayerInfo: Can't find Camera!");
    }

    public static GameObject GetPlayer()
    {
      return player;
    }

    public static Camera GetMainCamera()
    {
      return mainCamera;
    }
  }
}
