using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using UnityEngine;

namespace RPG.Combat
{
  public class ProjectileCast : Ability
  {
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] int projectileAmount;
    [SerializeField] int degrees;

    public Stack<Projectile> objectPool = new Stack<Projectile>();

    public override void CastAction()
    {
      float damage = CalculateDamage();

      Vector3 direction = data.lookPoint - transform.position;
      //middle projectile
      SpawnProjectile(direction, data.source,damage, data.castPosition, data.layer);

      float spawnDegrees = CalculateSpawnDegree();
      Vector3 newDirection;
      for (int i = 1; i <= (projectileAmount - 1) / 2; ++i)
      {
        //right hand projectiles
        newDirection = Quaternion.AngleAxis(spawnDegrees * i, Vector3.up) * direction;
        SpawnProjectile(newDirection, data.source, damage, data.castPosition, data.layer);
        //left hand projectiles
        newDirection = Quaternion.AngleAxis(-spawnDegrees * i, Vector3.up) * direction;
        SpawnProjectile(newDirection, data.source, damage, data.castPosition, data.layer);
      }
    }

    public void RepoolProjectile(Projectile projectile)
    {
      objectPool.Push(projectile);
    }

    public void ClearPool()
    {
      while (objectPool.Count > 0)
      {
        Destroy(objectPool.Pop().gameObject);
      }
    }

    void SpawnProjectile(Vector3 direction, GameObject source, float damage, Transform castPosition, LayerMask layer)
    {
      direction.y = 0;
      Projectile projectile = GetProjectile();
      projectile.Initialize(direction, source, this, castPosition.position, damage, base.range, base.damageType, layer);
    }

    CharacterStats stats;
    private float CalculateDamage()
    {
      if (stats == null) data.source.GetComponent<CharacterStats>();

      float damage;
      if (damageType == DamageType.physicalDamage)
        damage = (baseEffectValue + stats.GetStat(Stat.DamageFlat)) * (1 + stats.GetStat(Stat.DamagePercent) / 100f);
      else
        damage = (baseEffectValue + stats.GetStat(Stat.MagicDamageFlat)) * (1 + stats.GetStat(Stat.MagicDamagePercent) / 100f);

      return damage;
    }

    Projectile GetProjectile()
    {
      Projectile projectile;
      if (objectPool.Count == 0) projectile = Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
      else projectile = objectPool.Pop();

      return projectile;
    }

    float CalculateSpawnDegree()
    {
      if (projectileAmount < 2) return 0;
      return degrees / projectileAmount - 1;
    }
  }
}
