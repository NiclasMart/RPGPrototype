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
      // Vector3 translation = Vector3.Normalize(data.lookPoint - transform.parent.position) * moveDistance;
      Vector3 cursorPoint = PlayerInfo.GetPlayerCursor().Position;
      cursorPoint.y = data.source.transform.position.y;
      Vector3 direction = Vector3.Normalize(cursorPoint - data.source.transform.position);

      transform.parent.GetComponent<NavMeshAgent>().destination = data.source.transform.position + direction * moveDistance;
      float defaultSpeed = transform.parent.GetComponent<NavMeshAgent>().speed;
      transform.parent.GetComponent<NavMeshAgent>().speed = defaultSpeed * 2;
      yield return new WaitForSeconds(time);
      transform.parent.GetComponent<NavMeshAgent>().speed = defaultSpeed;


      // while (transform.parent.GetComponent<NavMeshAgent>().remainingDistance > 0)
      // {
      //   yield return new WaitForEndOfFrame();
      // }
      // transform.parent.GetComponent<NavMeshAgent>().destination = transform.parent.position;
      // Vector3 translation = Vector3.Normalize(data.lookPoint - transform.parent.position) * moveDistance;
      // float steps = time / Time.fixedDeltaTime;
      // Vector3 stepSize = translation / steps;

      // int count = 0;
      // while (count < steps)
      // {
      //   //transform.parent.GetComponent<NavMeshAgent>().destination = transform.parent.position;
      //   transform.parent.GetComponent<NavMeshAgent>().Move(stepSize);
      //   count++;
      //   yield return new WaitForSeconds(Time.fixedDeltaTime);
      // }
    }
  }
}


