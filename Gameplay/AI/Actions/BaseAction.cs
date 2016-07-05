using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class ActionData
    {
        public BaseAction waiterForAi;
        public GameObject lastGo;
        public ActionData()
        {

        }

    }
    public class BaseAction : MonoBehaviour
    {
        public virtual void ActionsEnd(PawnAI pawnAI)
        {
        }

        public virtual void StartAction(PawnAI pawnAI, GameObject go)
        {
        }

        public virtual bool IsOneFrameAction()
        {
            return true;
        }

        public virtual bool CheckTarget(ActorDescriptor actorDescr)
        {
            return false;
        }


        public virtual void FinishAction(AIEvent aIEvent, PawnAI pawnAI)
        {           
        }

        public virtual bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            return true;
        }
    }
    public abstract class ActionWithDuration : BaseAction
    {
        public override bool IsOneFrameAction()
        {
            return false;
        }
    }
}
