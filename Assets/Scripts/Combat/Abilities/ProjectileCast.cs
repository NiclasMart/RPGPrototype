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

    public override void Cast(Vector3 direction, GameObject source, Transform castPosition)
    {
      print(objectPool.Count);
      SpawnArrow(direction, source, castPosition);

      float spawnDegrees = CalculateSpawnDegree();
      Vector3 newDirection;
      for (int i = 1; i <= (projectileAmount - 1) / 2; ++i)
      {
        newDirection = Quaternion.AngleAxis(spawnDegrees * i, Vector3.up) * direction;
        SpawnArrow(newDirection, source, castPosition);

        newDirection = Quaternion.AngleAxis(-spawnDegrees * i, Vector3.up) * direction;
        SpawnArrow(newDirection, source, castPosition);
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

    void SpawnArrow(Vector3 direction, GameObject source, Transform castPosition)
    {
      direction.y = 0;
      GetProjectile().Initialize(direction, this, castPosition.position, base.baseDamage, base.range);
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
