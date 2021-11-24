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
    public float staminaConsumption;

    public AlterValue<float> alterStamina;

    public override bool CastIsValid()
    {
      Stamina stamina = data.source.GetComponent<Stamina>();
      float usedStamina = staminaConsumption;
      alterStamina?.Invoke(ref usedStamina);
      return stamina.UseStamina(usedStamina);
      // if (alterStamina != null) return stamina.UseStamina(alterStamina.Invoke(staminaConsumption));
      // else return stamina.UseStamina(staminaConsumption);
    }

    public override void CastAction()
    {
      StartCoroutine(Dash());
    }

    IEnumerator Dash()
    {
      Vector3 direction = GetDashDirection();

      NavMeshAgent agent = transform.parent.GetComponent<NavMeshAgent>();
      agent.destination = data.source.transform.position + direction * moveDistance;
      float speedChange = agent.speed;
      agent.speed += speedChange;

      yield return new WaitForSeconds(time);

      agent.speed -= speedChange;
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


