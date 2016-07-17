using UnityEngine;
using System.Collections;

namespace AI
{
    public class StopMoving : BaseAction
    {
        protected override void _StartAction(Context context)
        {
            context.pawnAI.CancelNavigation();
        }
    }
}
