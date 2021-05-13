using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
  public class Ability : MonoBehaviour
  {
    public string name;
    public virtual void Cast(){}
  }
}
