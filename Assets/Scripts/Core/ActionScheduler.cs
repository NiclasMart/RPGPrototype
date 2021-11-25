using UnityEngine;

namespace RPG.Core
{
  public class ActionScheduler : MonoBehaviour
  {
    IAction currentAction;
    bool lastActionBlocks;
    public bool StartAction(IAction action, bool block)
    {
      if (lastActionBlocks) return false;
      if (currentAction != action)
      {
        if (currentAction != null) currentAction.Cancel();
        currentAction = action;
        lastActionBlocks = block;
      }
      return true;
    }

    public void ReleaseLock()
    {
      lastActionBlocks = false;
    }

    public void CancelCurrentAction()
    {
      StartAction(null, false);
    }
  }
}