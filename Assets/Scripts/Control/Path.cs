using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
  public class Path : MonoBehaviour
  {
    [SerializeField] List<Transform> path;

    public int WaypointCount => path.Count;

    public int GetClosestWaypoint(Vector3 position)
    {
      int closestPoint = 0;
      float smallestDistance = Mathf.Infinity;

      for (int i = 0; i < path.Count; i++)
      {
        float distance = Vector3.Distance(position, path[i].position);
        if (distance < smallestDistance)
        {
          smallestDistance = distance;
          closestPoint = i;
        }
      }

      return closestPoint;
    }

    public Vector3 GetCurrentWaypoint(int index)
    {
      return path[index].position;
    }

    public Vector3 GetNextWaypoint(int index)
    {
      return (index != path.Count - 1) ? path[index + 1].position : path[0].position;
    }

    private void OnDrawGizmosSelected()
    {
      Gizmos.color = Color.white;
      for (int i = 0; i < path.Count; i++)
      {
        Gizmos.DrawSphere(path[i].position, 0.2f);
        Vector3 nextPoint = GetNextWaypoint(i);
        Gizmos.DrawLine(path[i].position, nextPoint);
      }
    }
  }
}
