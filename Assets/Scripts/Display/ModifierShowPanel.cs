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
      if (item == null)
      {
        title.text = "No Gear Equiped!";
        return;
      }

      DisplayMainInfo(item);
      DisplayModifiers(item);
    }

    public void Clear()
    {
      ClearText();
      SetIcon(null, defaultSlotCol);
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

      SetIcon(item.icon, ModifiableItem.GetRarityColor(item.rarity));
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

    private void SetIcon(Sprite itemSprite, Color rarity)
    {
      iconSlot.color = rarity;
      icon.sprite = itemSprite;
    }

    private void ClearText()
    {
      title.text = "";
      mainStats.text = "";
      foreach (var display in modifiers.GetComponentsInChildren<TextMeshProUGUI>())
      {
        display.text = "";
      }
    }
  }
}