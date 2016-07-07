using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToAction : ActionWithDuration
    {

        public override void StartAction(Context context)
        {
            context.pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(context.go.transform.position));
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
