using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using UltEvents;

namespace RPG.Interaction
{
  public class UIActivator : Interactable
  {
    [SerializeField] List<GameObject> UI = new List<GameObject>();
    [SerializeField] float openDistance = 2f;
    [SerializeField] UltEvent openAction, closeAction;

    GameObject player;
    public GameObject Interacter => player;

    bool uiActive = false;

    private void Start()
    {
      SetUIActive(false);
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
      SetUIActive(uiActive);

      if (uiActive) openAction.Invoke();
      else closeAction.Invoke();
    }

    private void SetUIActive(bool on)
    {
      foreach (var ui in UI)
      {
        ui.SetActive(on);
      }
    }

    public override void Interact(GameObject interacter)
    {
      player = interacter;
      ToggleUI();
    }
  }
}
