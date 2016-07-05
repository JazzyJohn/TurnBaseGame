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
        public override void StartAction(PawnAI pawnAI, GameObject go)
        {

            ActionSequenceData data = pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if(data == null)
            {
                return;
            }
            data.lastGo = go;
            DoAction(pawnAI, go, data);

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
        public override void FinishAction(AIEvent aiEvent, PawnAI pawnAI)
        {
            ActionSequenceData data = pawnAI.CreateOrAquireData<ActionSequenceData>(CreateData);
            if (data == null)
            {
                return;
            }
            actions[data.currentIndex].FinishAction(aiEvent, pawnAI);
            data.currentIndex++;
            DoAction(pawnAI, data.lastGo, data);


        }
        public virtual bool ShouldDoAction(int index, PawnAI pawnAi, GameObject go)
        {
            return true;
        }
        private void DoAction(PawnAI pawnAI, GameObject go, ActionSequenceData data)
        {
            while (data.currentIndex < actions.Length )
            {
                if (ShouldDoAction(data.currentIndex, pawnAI, go))
                {
                    actions[data.currentIndex].StartAction(pawnAI, go);
                    Debug.Log(data.currentIndex + " " + actions[data.currentIndex]);
                    if (!actions[data.currentIndex].IsOneFrameAction())
                        break;
                }        

                data.currentIndex++;
            }

            if (data.currentIndex >= actions.Length)
            {
                pawnAI.ClearOfAnyData();
            }
            else
            {
                pawnAI.AddActionCallback(this);
            }
        }
    }
}
