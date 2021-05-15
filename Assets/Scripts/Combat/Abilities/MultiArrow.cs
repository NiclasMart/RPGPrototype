using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
  public class MultiArrow : Ability
  {
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] int projectileAmount;
    [SerializeField] int degrees;

    public override void Cast(Vector3 direction, GameObject source, Transform castPosition)
    {
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

    void SpawnArrow(Vector3 direction, GameObject source, Transform castPosition)
    {
      GameObject arrowInstance = Instantiate(arrowPrefab, castPosition.position, Quaternion.identity);
      direction.y = 0;
      arrowInstance.GetComponent<Projectile>().Initialize(direction, source, base.baseDamage, base.range);
    }

    float CalculateSpawnDegree()
    {
      if (projectileAmount < 2) return 0;
      return degrees / projectileAmount - 1;
    }
  }
}
