using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToAction : ActionWithDuration
    {

        public override void StartAction(PawnAI pawnAI, GameObject go)
        {
            pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(go.transform.position));
        }

        public override bool CheckTarget(Descriptor.ActorDescriptor actorDsecr)
        {
            return true;
        }
        public override bool IsSuitableEvent(AIEvent aIEvent, PawnAI pawnAI)
        {
            if(aIEvent == AIEvent.NavigationComplited)
            {
                return true;
            }
            return false;
        }
    }
}
