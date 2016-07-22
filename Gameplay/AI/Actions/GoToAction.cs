using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToAction : BaseAction
    {
        public float reachDistance;

        protected override bool DoAction(Context context)
        {
            context.pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(context.go.transform.position), reachDistance, context.madeForUrgentPoints);
            return false;
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
