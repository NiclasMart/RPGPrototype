using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Interaction;
using RPG.Items;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace RPG.Display
{
  public class ButtonClickHandler : MonoBehaviour, IPointerClickHandler
  {
    public void OnPointerClick(PointerEventData eventData)
    {
      if (eventData.button == PointerEventData.InputButton.Right)
      {
        GetComponentInParent<ItemSlot>().HandleRightClick();
      }
    }
  }
}
