using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using System;
using System.Collections.Generic;

namespace RPG.Display
{
  public class ModifierDisplay : MonoBehaviour
  {
    [SerializeField] List<ModifierShowPanel> panels = new List<ModifierShowPanel>();
    [SerializeField] RectTransform canvasRect;
    GameObject graficComponent;

    int activeConnections = 0;
    bool uiIsActive = false;

    private void Awake()
    {
      graficComponent = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
      graficComponent.SetActive(false);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Tab))
      {
        if (uiIsActive) SetActive(false);
        else if (activeConnections > 0) SetActive(true);
      }
    }

    public void ShowModifiers(Item item)
    {
      ModifiableItem modItem = item as ModifiableItem;
      if (modItem == null) return;


      //ClearDisplay();
      //SetText(modItem);
      if (!uiIsActive)
      {
        SetPosition();
        panels[0].gameObject.SetActive(true);
      }
    }

    private void SetPosition()
    {
      if (Input.mousePosition.x > canvasRect.rect.width / 2) panels[0].transform.position = Input.mousePosition - new Vector3(GetComponent<RectTransform>().rect.width, 0, 0);
      else panels[0].transform.position = Input.mousePosition;
    }

    private void ClearDisplay()
    {
      // foreach (var field in grafic.GetComponentsInChildren<TextMeshProUGUI>())
      // {
      //   field.text = "";
      //   field.color = Color.white;
      // }
    }

    public void HideModifiers()
    {
      SetActive(false);
    }

    private void SetText(ModifierShowPanel panel, ModifiableItem modItem)
    {
      TextMeshProUGUI[] fields = panel.GetComponentsInChildren<TextMeshProUGUI>();
      for (int i = 0; i < modItem.modifiers.Count; i++)
      {
        ModifiableItem.Modifier modifier = modItem.modifiers[i];
        fields[i].text = modifier.GetDisplayText();

        if (modifier.rarity == Rank.Epic) fields[i].color = PlayerInfo.GetGlobalParameters().epic;
        else if (modifier.rarity == Rank.Legendary) fields[i].color = PlayerInfo.GetGlobalParameters().legendary;
      }
    }

    public void RegisterMenu()
    {
      activeConnections++;
    }

    public void UnregisterMenu()
    {
      activeConnections--;
      if (activeConnections == 0) SetActive(false);
    }


    public void SetActive(bool active)
    {
      uiIsActive = active;
      graficComponent.SetActive(active);
    }
  }
}
