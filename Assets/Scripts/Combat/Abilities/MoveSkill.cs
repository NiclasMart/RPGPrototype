using System.Collections;
using RPG.Core;
using RPG.Stats;
using UnityEngine;
using UnityEngine.AI;
using static Effect;

namespace RPG.Combat
{
  public class MoveSkill : Ability
  {
    public float moveDistance;
    public float time;
    public float staminaConsumption;

    public AlterStat alterStamina;

    public override bool CastIsValid()
    {
      Stamina stamina = data.source.GetComponent<Stamina>();
      if (alterStamina != null) return stamina.UseStamina(alterStamina.Invoke(staminaConsumption));
      else return stamina.UseStamina(staminaConsumption);
    }

    public override void CastAction()
    {
      StartCoroutine(Dash());
      base.CastAction();
    }

    IEnumerator Dash()
    {
      Vector3 direction = GetDashDirection();

      NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
      agent.destination = data.source.transform.position + direction * moveDistance;
      float defaultSpeed = agent.speed;
      agent.speed = defaultSpeed * 2;

      yield return new WaitForSeconds(time);

      agent.speed = defaultSpeed;
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


