using UnityEngine;
using System.Collections;
using Descriptor;
using System.Collections.Generic;

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
    public enum SourceOfAction
    {
        Nospecified,
        UI,
        Event,

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
        List<GameObject> additionalObjects;
        public bool allowSwitchTarget;
        public SourceOfAction source = SourceOfAction.Nospecified;
        public void AddToAdditional(GameObject go)
        {
            if (additionalObjects == null)
                additionalObjects = new List<GameObject>();
            additionalObjects.Remove(go);
        }
        public List<GameObject> GetAdditional()
        {
            return additionalObjects;
        }
    }
    public class BaseAction : MonoBehaviour
    {
        public bool switchTarget = false;
        public virtual void ActionsEnd(PawnAI pawnAI)
        {
        }

        public void StartAction(Context context)
        {
            _StartAction(context);
            if(!IsOneFrameAction())
            {
                
                ActionService.CreateNewData maker = delegate()
                {
                    ActionData actionData = new ActionData();                   
                    return actionData;
                };
                ActionData data = context.pawnAI.CreateOrAquireData<ActionData>(maker);
                Debug.Log("newly create data" + data);
                data.context = context;
                context.pawnAI.AddActionCallback(this);
            }            
        }

        protected virtual void _StartAction(Context context)
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


        public virtual bool FinishAction(AIEvent aIEvent, PawnAI pawnAI)
        {
            return true;
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
