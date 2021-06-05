using RPG.Display;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Interaction
{
  public class ItemSlot : MonoBehaviour
  {
    [SerializeField] Image iconSlot;
    [SerializeField] Button selectable;
    [HideInInspector] public Item item { get; private set; }
    Inventory inventory;
    Color stdColor;

    private void Awake()
    {
      stdColor = selectable.GetComponent<Image>().color;
    }

    public void Initialize(Item item, Inventory inventory)
    {
      SetIcon(item.icon);
      this.item = item;
      this.inventory = inventory;
    }

    public void Select()
    {
      SetColor(selectable.colors.highlightedColor);
      inventory.SelectSlot(this);
    }

    public void Deselect()
    {
      SetColor(stdColor);
    }

    void SetIcon(Sprite icon)
    {
      iconSlot.sprite = icon;
    }

    void SetColor(Color color)
    {
      selectable.GetComponent<Image>().color = color;
    }

  }
}
