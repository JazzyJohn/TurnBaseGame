using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class TakeCover : BaseAction
    {
        protected override void _StartAction(Context context)
        {
            context.pawnAI.GetOwner().DoCover();
        }

        public override bool CheckTarget(ActorDescriptor actorDsecr)
        {
            return actorDsecr.IsCover;
        }
    }

}