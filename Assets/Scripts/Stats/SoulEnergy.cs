using System.Collections;
using System.Collections.Generic;
using RPG.Display;
using UnityEngine;

namespace RPG.Stats
{
  public class SoulEnergy : MonoBehaviour, IDisplayable
  {
    int killAmount = 0;
    public ValueChangeEvent valueChange;

    public void AddKill()
    {
      killAmount++;
      valueChange.Invoke(this);
    }

    public float GetCurrentValue()
    {
      return (float)killAmount;
    }

    public float GetMaxValue()
    {
      return 10f;
    }
  }
}
