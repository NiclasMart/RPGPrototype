using System.Collections;
using System.Collections.Generic;
using RPG.Core;
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
    public AnimationClip animationClip;
    public float animationRotationOffset;
    Animator animator;

    protected CastData data;
    protected class CastData
    {
      public Vector3 direction;
      public GameObject source;
      public Transform castPosition;
      public LayerMask layer;
      public CastData(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer)
      {
        this.direction = direction;
        this.source = source;
        this.castPosition = castPosition;
        this.layer = layer;
      }
    }

    public void PrepareCast(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer, Animator animator)
    {
      data = new CastData(direction, source, castPosition, layer);
      this.animator = animator;
    }

    public virtual void CastAction() { }

    public void Cancel() { }
  }
}
