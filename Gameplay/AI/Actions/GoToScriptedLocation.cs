using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToScriptedLocation : ActionWithDuration
    {
        public float reachDistance;
        public GameObject target;

        protected override void _StartAction(Context context)
        {
            if(target != null)
            {                
                context.pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(target.transform.position), reachDistance);
            }
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
