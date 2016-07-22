using UnityEngine;
using System.Collections;

namespace AI
{
    public class StopMoving : BaseAction
    {
        protected override bool DoAction(Context context)
        {
            context.pawnAI.CancelNavigation();
            return true;
        }
    }
}
