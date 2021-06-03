using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
  public class PlayerInfo : MonoBehaviour
  {
    static GameObject player;
    static Camera mainCamera;
    static PlayerCursor playerCursor;

    private void Awake()
    {
      player = GameObject.FindGameObjectWithTag("Player");
      if (!player) Debug.LogError("PlayerInfo: Can't find Player!");
      mainCamera = Camera.main;
      if (!mainCamera) Debug.LogError("PlayerInfo: Can't find Camera!");
      playerCursor = player.GetComponent<PlayerCursor>();
      if (!playerCursor) Debug.LogError("PlayerInfo: Can't find Player Cursor!");
    }

    public static GameObject GetPlayer()
    {
      return player;
    }

    public static Camera GetMainCamera()
    {
      return mainCamera;
    }

    public static PlayerCursor GetPlayerCursor()
    {
      return playerCursor;
    }
  }
}
