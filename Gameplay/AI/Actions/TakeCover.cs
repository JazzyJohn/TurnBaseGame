using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class TakeCover : BaseAction
    {
        public override void StartAction(Context context)
        {
            context.pawnAI.GetOwner().DoCover();
        }

        public override bool CheckTarget(ActorDescriptor actorDsecr)
        {
            return actorDsecr.IsCover;
        }
    }

}