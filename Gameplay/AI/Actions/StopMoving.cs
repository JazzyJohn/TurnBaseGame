using UnityEngine;
using System.Collections;

namespace AI
{
    public class StopMoving : BaseAction
    {
        public override void StartAction(Context context)
        {
            context.pawnAI.CancelNavigation();
        }
    }
}
