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
        protected override void _StartAction(Context context)
        {
            ActionSequenceData data = context.pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if(data == null)
            {
                Debug.Log("No Data for Sequence");
                return;
            }
            data.context = context;
            if(DoAction(context, data))
            {
                context.pawnAI.ClearOfAnyData();
            }

        }
        public override bool CheckTarget(Descriptor.ActorDescriptor actorDescr)
        {
            foreach(BaseAction action in actions)
            {
                if (!action.CheckTarget(actorDescr))
                    return false;
            }
            return true;
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
                return true;
            }
            actions[data.currentIndex].FinishAction(aiEvent, pawnAI);
            data.currentIndex++;
            return DoAction(data.context, data);


        }
        public override bool IsOneFrameAction()
        {
            foreach(BaseAction action in actions)
            {
                if(!action.IsOneFrameAction())
                {
                    return false;
                }
            }
            return true ;
        }
        public virtual bool ShouldDoAction(int index, PawnAI pawnAi, GameObject go)
        {
            return true;
        }
        private bool DoAction(Context context, ActionSequenceData data)
        {
            while (data.currentIndex < actions.Length )
            {
                if (ShouldDoAction(data.currentIndex, context.pawnAI, context.go))
                {
                    actions[data.currentIndex].StartAction(context);
                    if (!actions[data.currentIndex].IsOneFrameAction())
                        break;
                }        

                data.currentIndex++;
            }
            bool isFinished = data.currentIndex >= actions.Length;
     
            return isFinished;
        }
    }
}
