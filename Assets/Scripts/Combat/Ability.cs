using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using UnityEditor.Animations;
using UnityEngine;

namespace RPG.Combat
{
  public class Ability : MonoBehaviour, IAction
  {
    public string name;
    public float baseDamage;
    public float cooldown;
    public float range;
    public DamageClass damageType;
    public Sprite icon;
    public AnimationClip animationClip;
    public float animationRotationOffset;
    public bool castImmediately = false;

    protected CastData data;
    protected class CastData
    {
      public Vector3 lookPoint;
      public GameObject source;
      public Transform castPosition;
      public LayerMask layer;
      public CastData(Vector3 lookPoint, GameObject source, Transform castPosition, LayerMask layer)
      {
        this.lookPoint = lookPoint;
        this.source = source;
        this.castPosition = castPosition;
        this.layer = layer;
      }
    }

    public void PrepareCast(Vector3 lookPoint, GameObject source, Transform castPosition, LayerMask layer)
    {
      data = new CastData(lookPoint, source, castPosition, layer);
    }

    public void GetStats(ModifyTable stats)
    {
      stats.damageFlat = baseDamage;
      stats.attackSpeed = 1 / cooldown;
      stats.attackRange = range;
    }

    public virtual bool CastIsValid() { return true; }

    public virtual void CastAction() { }

    public void Cancel() { }
  }
}
