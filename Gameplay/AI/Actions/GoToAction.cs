﻿using UnityEngine;
using System.Collections;

namespace AI
{
    public class GoToAction : ActionWithDuration
    {
        public float reachDistance;

        protected override void _StartAction(Context context)
        {
            Debug.Log(context.pawnAI + " goto " + context.go);
            context.pawnAI.MoveTo(Grid.GridController.GetCellFromCoord(context.go.transform.position), reachDistance);
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
