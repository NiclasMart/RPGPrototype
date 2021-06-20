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
    [SerializeField] Image panelBackground;

    int activeConnections = 0;
    bool compareModeIsActive = false;
    Vector3 panelStartPosition;

    private void Start()
    {
      panelStartPosition = panels[1].GetComponent<RectTransform>().position;
      SetUIActive(false);
    }

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Tab)) SetCompareMode();
    }

    private void SetCompareMode()
    {
      if (compareModeIsActive)
      {
        compareModeIsActive = false;
        SetUIActive(false);
      }
      else if (activeConnections > 0)
      {
        panels[1].GetComponent<RectTransform>().position = panelStartPosition;
        compareModeIsActive = true;
        SetUIActive(true);
      }
    }

    public void ShowModifiers(Item item)
    {
      ModifiableItem modItem = item as ModifiableItem;
      if (modItem == null) return;


      //ClearDisplay();
      //SetText(modItem);
      if (!compareModeIsActive)
      {
        panels[1].SetActive(true);
        SetPosition();
        panels[1].DisplayModifiers(modItem);
      }
    }

    private void SetPosition()
    {
      if (Input.mousePosition.x > canvasRect.rect.width / 2) panels[1].transform.position = Input.mousePosition - new Vector3(GetComponent<RectTransform>().rect.width, 0, 0);
      else panels[1].transform.position = Input.mousePosition;
    }

    private void ClearDisplay()
    {
      // foreach (ModifierShowPanel panel in panels)
      // {
      //   if (!panel.isActiveAndEnabled) continue;

      //   for
      //   field.text = "";
      //   field.color = Color.white;
      // }
    }

    public void HideModifiers()
    {
      if (compareModeIsActive) return;
      panels[1].gameObject.SetActive(false);
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
      if (activeConnections == 0) SetUIActive(false);
    }


    public void SetUIActive(bool active)
    {
      panelBackground.gameObject.SetActive(active);
      foreach (var panel in panels)
      {
        panel.SetActive(active);
      }
    }


  }
}
