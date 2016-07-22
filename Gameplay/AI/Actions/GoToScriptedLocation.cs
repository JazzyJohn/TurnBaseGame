using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToScriptedLocation : BaseAction
    {
        public float reachDistance;
        public GameObject target;

        protected override bool DoAction(Context context)
        {
            if(target != null)
            {
                context.pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(target.transform.position), reachDistance, context.madeForUrgentPoints);
            }
            return false;
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
