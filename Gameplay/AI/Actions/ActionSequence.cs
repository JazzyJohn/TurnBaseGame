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

            if( data.currentIndex >= actions.Length)
            {
                pawnAI.ClearOfAnyData();
            }
            else
            {
                actions[data.currentIndex].StartAction(pawnAI, go);
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
    }
}
