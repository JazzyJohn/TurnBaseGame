using UnityEngine;
using System.Collections;

namespace AI
{
    public class ActionSequenceData: ActionData
    {
        public int currentIndex = 0;
    }
    public class ActionSequence : ActionWrapper
    {
        public ActionData CreateData()
        {
            return new ActionSequenceData();
        }
        protected override bool DoAction(Context context)
        {
            ActionSequenceData data = context.pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if(data == null)
            {
                Debug.Log("No Data for Sequence");
                return true;
            }
            data.context = context;
            if (_DoAction(context, data))
            {

                context.pawnAI.ClearOfAnyData();
                return true;
            }
            return false;
        }
        public override bool CheckTarget(Descriptor.ActorDescriptor actorDescr)
        {
            foreach(BaseAction action in actions)
            {
                if (!action.CheckTarget(actorDescr))
                    return false;
            }
            return base.CheckTarget(actorDescr);
        }
        public override bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            ActionSequenceData data = pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if (data == null)
            {
                return true;
            }
            return actions[data.currentIndex].IsSuitableEvent(aIEvent, pawnAI);
        }
        public override bool FinishAction(AIEvent aiEvent, PawnAI pawnAI)
        {
            ActionSequenceData data = pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if (data == null)
            {
                Debug.LogError("No data for ActionSequence:" + this);
                return true;
            }
            Debug.Log("Continue;" + pawnAI);
            actions[data.currentIndex].FinishAction(aiEvent, pawnAI);
            
            data.currentIndex++;
            return _DoAction(data.context, data);


        }
      
        public virtual bool ShouldDoAction(int index, Context context)
        {
            return true;
        }
        private bool _DoAction(Context context, ActionSequenceData data)
        {

            while (data.currentIndex < actions.Length )
            {
                
                if (ShouldDoAction(data.currentIndex, context))
                {
                    
                    if (!actions[data.currentIndex].StartAction(context))
                    {
                        context.pawnAI.AddActionCallback(this);
                        break;
                    }
                }        

                data.currentIndex++;
            }
            bool isFinished = data.currentIndex >= actions.Length;
     
            return isFinished;
        }
    }
}
