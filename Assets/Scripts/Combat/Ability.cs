using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace RPG.Combat
{
  public class Ability : MonoBehaviour
  {
    public string name;
    public float baseDamage;
    public float cooldown;
    public float range;
    public DamageClass damageType;
    public AnimationClip animationClip;
    public virtual void Cast(Vector3 direction, GameObject source, Transform castPosition, LayerMask layer) { }

    public void OverrideAnimations(Animator animator, string clipName)
    {
      float testTime = Time.time;
      if (animationClip != null)
      {
        AnimatorOverrideController aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);

        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        var oldAnims = new List<KeyValuePair<AnimationClip, AnimationClip>>();

        AnimatorController controller;
        if (animator.runtimeAnimatorController is AnimatorController)
        {
          controller = animator.runtimeAnimatorController as AnimatorController;
          UnityEditor.Animations.AnimatorControllerLayer[] layers = controller.layers;

          foreach (var state in layers[0].stateMachine.states)
          {
            Motion m = state.state.motion;
            if (m is BlendTree)
            {
              BlendTree tree = m as BlendTree;
              foreach (ChildMotion motion in tree.children)
              {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(motion.motion as AnimationClip, motion.motion as AnimationClip));
              }
              continue;
            }
            if (state.state.name == clipName)
              anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, animationClip));
            else
              anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip));
          }
        }
        else
        {
          AnimatorOverrideController overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
          overrideController.GetOverrides(oldAnims);
          controller = overrideController.runtimeAnimatorController as AnimatorController;

          UnityEditor.Animations.AnimatorControllerLayer[] layers = controller.layers;
          foreach (var state in layers[0].stateMachine.states)
          {
            Motion m = state.state.motion;
            if (m is BlendTree)
            {
              BlendTree tree = m as BlendTree;
              foreach (ChildMotion motion in tree.children)
              {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(motion.motion as AnimationClip, motion.motion as AnimationClip));
              }
              continue;
            }
            if (state.state.name == clipName)
              anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, animationClip));
            else
            {
              if (oldAnims.Contains(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip)))
              {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip));
              }
              else
              {
                anims.Add(oldAnims.Find(kvp => (kvp.Key as AnimationClip).name == m.name));
              }

            }
          }
        }
        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;
      }
    }
  }
}
