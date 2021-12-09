using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Items;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  [Serializable] public enum DamageType { physicalDamage, magicDamage }
  public class Ability : MonoBehaviour
  {
    [HideInInspector] public string name;
    public string description;
    public DamageType damageType;
    [HideInInspector] public float baseEffectValue;
    public float cooldown;
    public float staminaConsumption;
    public float range;
    public Sprite icon;
    public AnimationClip animationClip;
    public bool shouldRotate = true;
    public float animationRotationOffset;
    public bool castImmediately = false;
    public bool hasUses = false;
    public int useAmount = 2;
    [HideInInspector] public int remainingUses = -1;

    public Action onEndCast;

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
      stats.attackSpeed = 1;
      stats.attackRange = range;
    }

    public virtual bool CastIsValid(GameObject player)
    {
      Stamina stamina = player.GetComponent<Stamina>();
      return stamina.UseStamina(staminaConsumption);
    }

    public virtual void CastAction() { }
  }
}
