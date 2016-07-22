using UnityEngine;
using System.Collections;

namespace AI
{
    public class ClearDelayReaction : BaseAction
    {
        public ReactionPriority maxReactionPriority;
        protected override bool DoAction(Context context)
        {
            EventHandler handler = null;
            if (context.allowSwitchTarget && switchTarget)
            {
                PawnAI pawnAI = context.go.GetComponent<PawnAI>();
                if(pawnAI != null)
                {
                    handler = pawnAI.GetEventHandler();
                }
            }
            else
            {
                handler = context.pawnAI.GetEventHandler();
            }

            if(handler != null)
            {
                handler.ClearDelayReaction(maxReactionPriority);
            }
            return true;
        }
    }
}
