using System.Collections.Generic;
using UltEvents;


namespace RPG.Items
{
  public class ModifiableItem : Item
  {
    public class Modifier
    {
      public int value;
      public string display;
      public UltEvent effect;

      public Modifier(ItemStatModifier baseModifier)
      {
        value = baseModifier.GetRandomValue();
        display = baseModifier.displayText;
        effect += baseModifier.effect.Invoke;
        effect.Invoke();
      }

      public string GetDisplayText()
      {
        return display.Replace("*", value.ToString());
      }
    }

    public List<Modifier> modifiers = new List<Modifier>();
    public ModifiableItem(GenericItem baseItem) : base(baseItem) { }

    public void AddModifier(ItemStatModifier newModifier)
    {
      modifiers.Add(new Modifier(newModifier));
    }
  }
}