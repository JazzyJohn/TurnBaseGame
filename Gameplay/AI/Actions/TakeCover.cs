using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class TakeCover : BaseAction
    {
        public override void StartAction(PawnAI pawnAI, GameObject go)
        {
            pawnAI.GetOwner().DoCover();
        }

        public override bool CheckTarget(ActorDescriptor actorDsecr)
        {
            return actorDsecr.IsCover;
        }
    }

}