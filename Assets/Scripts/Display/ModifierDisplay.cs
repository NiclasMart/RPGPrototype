using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using System;

namespace RPG.Display
{
  public class ModifierDisplay : MonoBehaviour
  {
    [SerializeField] RectTransform canvasRect;
    CanvasGroup graficComponent;
    Transform panel;
    Color epicCol, legendaryCol;

    private void Awake()
    {
      graficComponent = GetComponent<CanvasGroup>();
      panel = transform.GetChild(0);
    }

    private void Start()
    {
      SetDisplay(false);
    }

    public void ShowModifiers(Item item)
    {
      ModifiableItem modItem = item as ModifiableItem;
      if (modItem == null) return;

      SetPosition();

      ClearDisplay();
      SetText(modItem);
      SetDisplay(true);
    }

    private void SetPosition()
    {
      if (Input.mousePosition.x > canvasRect.rect.width / 2) transform.position = Input.mousePosition - new Vector3(GetComponent<RectTransform>().rect.width, 0, 0);
      else transform.position = Input.mousePosition;
    }

    private void ClearDisplay()
    {
      foreach (var field in panel.GetComponentsInChildren<TextMeshProUGUI>())
      {
        field.text = "";
        field.color = Color.white;
      }
    }

    public void HideModifiers()
    {
      SetDisplay(false);
    }

    private void SetText(ModifiableItem modItem)
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

    private void SetDisplay(bool active)
    {
      if (active) graficComponent.alpha = 1f;
      else graficComponent.alpha = 0;
    }
  }
}
