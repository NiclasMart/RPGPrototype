using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Interaction
{
  public class UIActivator : Interactable
  {
    [SerializeField] GameObject UI;
    public GameObject connectedUI => UI;
    [SerializeField] float openDistance = 2f;

    GameObject player;
    public GameObject Interacter => player;

    bool uiActive = false;

    private void Start()
    {
      UI.SetActive(false);
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
      player = interacter;
      ToggleUI();
    }
  }
}
