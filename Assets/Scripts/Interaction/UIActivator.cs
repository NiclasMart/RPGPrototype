using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Interaction
{
  public class UIActivator : Interactable
  {
    [SerializeField] GameObject UI;
    [SerializeField] float openDistance = 2f;
    GameObject player;
    
    bool uiActive = false;

    private void Start()
    {
      UI.SetActive(false);
      player = PlayerInfo.GetPlayer();
    }

    private void Update()
    {
      if (uiActive && !PlayerInRange()) ToggleUI();
    }

    private bool PlayerInRange()
    {
      return Vector3.Distance(player.transform.position, transform.position) <= openDistance;
    }

    private void ToggleUI()
    {
      uiActive = !uiActive;
      UI.SetActive(uiActive);
    }

    public override void Interact(GameObject interacter)
    {
      ToggleUI();
    }
  }
}
