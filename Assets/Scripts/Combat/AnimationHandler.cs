using System.Collections.Generic;
// using UnityEditor.Animations;
using UnityEngine;

namespace RPG.Combat
{
  class AnimationHandler
  {
    public static void OverrideAnimations(Animator animator, AnimationClip animationClip, string clipName)
    {
      if (animationClip != null)
      {

        AnimatorOverrideController aoc;
        if (animator.runtimeAnimatorController is AnimatorOverrideController) aoc = animator.runtimeAnimatorController as AnimatorOverrideController;
        else aoc = new AnimatorOverrideController(animator.runtimeAnimatorController);

        var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
        var oldAnims = new List<KeyValuePair<AnimationClip, AnimationClip>>(aoc.overridesCount);
        aoc.GetOverrides(oldAnims);

        foreach (KeyValuePair<AnimationClip, AnimationClip> overridePair in oldAnims)
        {
          if (overridePair.Key.name == clipName) anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(overridePair.Key, animationClip));
          else anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(overridePair.Key, overridePair.Value));
        }

        aoc.ApplyOverrides(anims);
        animator.runtimeAnimatorController = aoc;


        // //differenciate if current animation controller is already an animation override controller
        // RuntimeAnimatorController controller;
        // if (animator.runtimeAnimatorController is RuntimeAnimatorController)
        // {
        //   controller = animator.runtimeAnimatorController as RuntimeAnimatorController;
        //   //UnityEditor.Animations.AnimatorControllerLayer[] layers = controller.layers;

        //   foreach (var clip in controller.animationClips)
        //   {
        //     //if animatin state motion is blend tree, copy animation without check (blend tree montions cant be changed at the moment)
        //     // Motion m = state.state.motion;
        //     // if (m == null) continue;
        //     // if (m is BlendTree)
        //     // {
        //     //   BlendTree tree = m as BlendTree;
        //     //   foreach (ChildMotion motion in tree.children)
        //     //   {
        //     //     anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(motion.motion as AnimationClip, motion.motion as AnimationClip));
        //     //   }
        //     //   continue;
        //     // }
        //     // check if state should be changed
        //     if (clip.name == clipName)
        //       anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(clip, animationClip));
        //     else
        //       anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(clip, clip));
        //   }
        //   //iterate over all animator states
        //   // foreach (var state in layers[0].stateMachine.states)
        //   // {
        //   //   //if animatin state motion is blend tree, copy animation without check (blend tree montions cant be changed at the moment)
        //   //   Motion m = state.state.motion;
        //   //   if (m == null) continue;
        //   //   if (m is BlendTree)
        //   //   {
        //   //     BlendTree tree = m as BlendTree;
        //   //     foreach (ChildMotion motion in tree.children)
        //   //     {
        //   //       anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(motion.motion as AnimationClip, motion.motion as AnimationClip));
        //   //     }
        //   //     continue;
        //   //   }
        //   //   // check if state should be changed
        //   //   if (state.state.name == clipName)
        //   //     anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, animationClip));
        //   //   else
        //   //     anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip));
        //   // }
        // }
        // // else
        // // {
        // //   //handle animation override controller
        // //   AnimatorOverrideController overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
        // //   overrideController.GetOverrides(oldAnims);
        // //   controller = overrideController.runtimeAnimatorController as AnimatorController;

        // //   UnityEditor.Animations.AnimatorControllerLayer[] layers = controller.layers;
        // //   foreach (var state in layers[0].stateMachine.states)
        // //   {
        // //     Motion m = state.state.motion;
        // //     if (m == null) continue;
        // //     if (m is BlendTree)
        // //     {
        // //       BlendTree tree = m as BlendTree;
        // //       foreach (ChildMotion motion in tree.children)
        // //       {
        // //         anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(motion.motion as AnimationClip, motion.motion as AnimationClip));
        // //       }
        // //       continue;
        // //     }
        // //     if (state.state.name == clipName)
        // //       anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, animationClip));
        // //     else
        // //     {
        // //       //check if the animation was already overriden, if so: copy this animation override instead the base animation
        // //       if (oldAnims.Contains(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip)))
        // //       {
        // //         anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(m as AnimationClip, m as AnimationClip));
        // //       }
        // //       else
        // //       {
        // //         anims.Add(oldAnims.Find(kvp => (kvp.Key as AnimationClip).name == m.name));
        // //       }

        // //     }
        // //   }
        // // }
        // //apply all animations to the current animation controller
        // aoc.ApplyOverrides(anims);
        // animator.runtimeAnimatorController = aoc;
      }
    }
  }
}