using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
  public class MoveSkill : Ability
  {
    public float moveDistance;
    public float time;
    Vector3 currentPosition, goalPosition;

    public override void CastAction()
    {
      StartCoroutine(MoveTest());
    }

    IEnumerator MoveTest()
    {
      Vector3 cursorPoint = PlayerInfo.GetPlayerCursor().Position;
      cursorPoint.y = data.source.transform.position.y;
      Vector3 direction = Vector3.Normalize(cursorPoint - data.source.transform.position);

      transform.parent.GetComponent<NavMeshAgent>().destination = data.source.transform.position + direction * moveDistance;
      float defaultSpeed = transform.parent.GetComponent<NavMeshAgent>().speed;
      transform.parent.GetComponent<NavMeshAgent>().speed = defaultSpeed * 2;
      yield return new WaitForSeconds(time);
      transform.parent.GetComponent<NavMeshAgent>().speed = defaultSpeed;
    }
  }
}


