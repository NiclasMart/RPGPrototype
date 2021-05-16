using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
  public class ProjectileCast : Ability
  {
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] int projectileAmount;
    [SerializeField] int degrees;

    public Stack<Projectile> objectPool = new Stack<Projectile>();

    public override void Cast(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer)
    {
      //middle projectile
      SpawnArrow(direction, source, castPosition, layer);

      float spawnDegrees = CalculateSpawnDegree();
      Vector3 newDirection;
      for (int i = 1; i <= (projectileAmount - 1) / 2; ++i)
      {
        //right hand projectiles
        newDirection = Quaternion.AngleAxis(spawnDegrees * i, Vector3.up) * direction;
        SpawnArrow(newDirection, source, castPosition, layer);
        //left hand projectiles
        newDirection = Quaternion.AngleAxis(-spawnDegrees * i, Vector3.up) * direction;
        SpawnArrow(newDirection, source, castPosition, layer);
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

    void SpawnArrow(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer)
    {
      print((int)layer);
      direction.y = 0;
      Projectile projectile = GetProjectile();
      projectile.gameObject.layer = layer;
      projectile.Initialize(direction, this, castPosition.position, base.baseDamage, base.range);
    }

    Projectile GetProjectile()
    {
      Projectile projectile;
      if (objectPool.Count == 0) projectile = Instantiate(arrowPrefab, Vector3.zero, Quaternion.identity).GetComponent<Projectile>();
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
