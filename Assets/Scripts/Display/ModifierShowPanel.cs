using RPG.Items;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Display
{
  public class ModifierShowPanel : MonoBehaviour
  {
    [SerializeField] TextMeshProUGUI title, mainStats;
    [SerializeField] Transform modifiers;
    [SerializeField] Image iconSlot;
    Color epicCol, legendaryCol, defaultSlotCol;
    Image icon;

    private void Awake()
    {
      defaultSlotCol = iconSlot.color;
      icon = iconSlot.transform.GetChild(0).GetComponent<Image>();
      Clear();
    }

    public void DisplayItem(ModifiableItem item)
    {
      Clear();
      if (item == null) return;

      DisplayMainInfo(item);
      DisplayModifiers(item);
    }

    public void Clear()
    {
      title.text = "";
      mainStats.text = "";
      foreach (var display in modifiers.GetComponentsInChildren<TextMeshProUGUI>())
      {
        display.text = "";
      }

      iconSlot.color = defaultSlotCol;
      icon.sprite = null;
    }

    public void SetActive(bool active)
    {
      gameObject.SetActive(active);
    }

    private void DisplayMainInfo(ModifiableItem item)
    {
      title.color = ModifiableItem.GetRarityColor(item.rarity);
      title.text = item.GetTitleText();

      mainStats.color = ModifiableItem.GetRarityColor(Rank.Normal);
      mainStats.text = item.GetMainStatText();

      print("set color");
      iconSlot.color = ModifiableItem.GetRarityColor(item.rarity);
      icon.sprite = item.icon;
    }

    private void DisplayModifiers(ModifiableItem item)
    {
      TextMeshProUGUI[] fields = modifiers.GetComponentsInChildren<TextMeshProUGUI>();
      for (int i = 0; i < item.modifiers.Count; i++)
      {
        ModifiableItem.Modifier modifier = item.modifiers[i];
        fields[i].text = modifier.GetDisplayText();
        fields[i].color = ModifiableItem.GetRarityColor(modifier.rarity);
      }
    }
  }
}