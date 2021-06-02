using System.Collections;
using System.Collections.Generic;
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
      Vector3 translation = Vector3.Normalize(data.lookPoint - transform.parent.position) * moveDistance;
      transform.parent.GetComponent<NavMeshAgent>().Move(translation);
      yield return new WaitForEndOfFrame();
      transform.parent.GetComponent<NavMeshAgent>().destination = transform.parent.position;
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


