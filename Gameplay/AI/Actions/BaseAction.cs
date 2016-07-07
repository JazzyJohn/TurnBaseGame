using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class ActionData
    {
        public BaseAction waiterForAi;
        public Context context;
        public ActionData()
        {

        }

    }
    public class Context
    {
        public Context(PawnAI pawnAI, GameObject go)
        {
            this.pawnAI = pawnAI;
            this.go = go;
        }
        public PawnAI pawnAI;
        public GameObject go;
        public bool allowSwitchTarget;
    }
    public class BaseAction : MonoBehaviour
    {
        public bool switchTarget = false;
        public virtual void ActionsEnd(PawnAI pawnAI)
        {
        }

        public virtual void StartAction(Context context)
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
