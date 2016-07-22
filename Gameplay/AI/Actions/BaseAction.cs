using UnityEngine;
using System.Collections;
using Descriptor;
using System.Collections.Generic;
using AI.Conditions;

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
        public bool allowSwitchTarget = true;
        public bool madeForUrgentPoints = false;
        public SourceOfAction source = SourceOfAction.Nospecified;
        Dictionary<string, object> addData;
        public void AddToAdditional(GameObject go)
        {
            if (additionalObjects == null)
                additionalObjects = new List<GameObject>();
            additionalObjects.Add(go);
        }
        public List<GameObject> GetAdditional()
        {
            return additionalObjects;
        }
        public void AddToData(string key, object value)
        {
            if (addData == null)
                addData = new Dictionary<string, object>();
            addData[key] = value;
        }
        public object GetAddData(string key)
        {
            return addData[key];
        }
    }
    public class BaseAction : MonoBehaviour
    {
        public bool switchTarget = false;

        public TargetCondition condition;
        public virtual void ActionsEnd(PawnAI pawnAI)
        {
        }

        public bool StartAction(Context context)
        {
            if (!DoAction(context))
            {
                              
                ActionData data = context.pawnAI.CreateOrAquireData<ActionData>(maker);
                data.context = context;
                context.pawnAI.AddActionCallback(this);
                return false;
            }
            return true;
        }
        protected ActionService.CreateNewData maker = delegate()
        {
            ActionData actionData = new ActionData();
            return actionData;
        };

        protected virtual bool DoAction(Context context)
        {
            return true;
        }



        public virtual bool CheckTarget(ActorDescriptor actorDescr)
        {
            return true && ( condition == null || condition.Check(actorDescr));
        }


        public virtual bool FinishAction(AIEvent aIEvent, PawnAI pawnAI)
        {
            return true;
        }

        public virtual bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            return true;
        }

        public virtual void Reverse(Context context)
        {
            
        }
    }

    public abstract class AnimatedAction : BaseAction
    {
        public override bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            return aIEvent == AIEvent.AnimationFinished;
        }
    }
    public abstract class UntilInterruptAction : BaseAction
    {
        public enum InterruptionType
        {
            Default,
            Damage,
            Reaction
        }
        public InterruptionType interruptionType;
        public override bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            switch(interruptionType)
            {
                case InterruptionType.Default:
                    return aIEvent == AIEvent.InterruptEvent;                    
                case InterruptionType.Damage:
                    return  aIEvent == AIEvent.InterruptEvent || aIEvent == AIEvent.DamageEvent;
                case InterruptionType.Reaction:
                    return aIEvent == AIEvent.InterruptEvent || aIEvent == AIEvent.ReactionEvent;
            }
            return false;
           
        }
    }
}
