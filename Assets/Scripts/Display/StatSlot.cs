using RPG.Stats;
using RPG.Items;
using UnityEngine;
using TMPro;

namespace RPG.Display
{
  public class StatSlot : MonoBehaviour
  {
    public Stat stat;
    [SerializeField] string format;

    public void SetStatDisplay(float value)
    {
      GetComponent<TextMeshProUGUI>().text = format.Replace("*", value.ToString());
    }
  }
}
