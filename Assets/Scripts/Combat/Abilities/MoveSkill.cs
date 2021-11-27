using System.Collections;
using RPG.Core;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
  public class MoveSkill : Ability
  {
    public float moveDistance;
    public float time;

    public AlterValue<float> alterStamina;

    public override bool CastIsValid(GameObject player)
    {
      Stamina stamina = player.GetComponent<Stamina>();
      float usedStamina = staminaConsumption;
      alterStamina?.Invoke(ref usedStamina);
      return stamina.UseStamina(usedStamina);
    }

    public override void CastAction()
    {
      StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
      Vector3 direction = GetDashDirection();

      NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
      float initialAngularSpeed = agent.angularSpeed;
      agent.isStopped = true;
      agent.angularSpeed = 0;
      float startTime = Time.time;

      while (Time.time < startTime + time)
      {
        agent.Move(direction * Time.deltaTime * moveDistance);
        yield return new WaitForEndOfFrame();
      }
      agent.destination = data.source.transform.position;
      agent.isStopped = false;
      agent.angularSpeed = initialAngularSpeed; ;

      onEndCast?.Invoke();
    }

    private Vector3 GetDashDirection()
    {
      Vector3 cursorPoint = PlayerInfo.GetPlayerCursor().Position;
      cursorPoint.y = data.source.transform.position.y;
      Vector3 direction = Vector3.Normalize(cursorPoint - data.source.transform.position);
      return direction;
    }
  }
}


