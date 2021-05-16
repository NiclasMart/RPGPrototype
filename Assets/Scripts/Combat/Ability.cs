using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
  public class Ability : MonoBehaviour
  {
    public string name;
    public float baseDamage;
    public float cooldown;
    public float range;
    public DamageClass damageType;
    public virtual void Cast(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer) { }
  }
}
