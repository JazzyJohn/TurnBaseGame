using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class TakeCover : BaseAction
    {
        protected override bool DoAction(Context context)
        {
            context.pawnAI.GetOwner().DoCover();
            return true;
        }

        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            return actorDescr.IsCover && base.CheckTarget(actorDescr);
        }
    }

}