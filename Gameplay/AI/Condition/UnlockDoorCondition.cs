using UnityEngine;
using System.Collections;
using Interaction;

namespace AI.Conditions
{
    [CreateAssetMenu(fileName = "UnlockDoorCondition", menuName = "AI/AIConditions/UnlockDoorCondition", order = 1)]
    public class UnlockDoorCondition : AICondition
    {

        protected override bool _CheckAI(Context context)
        {
            Door door = context.go.GetComponent<Door>();
            return door != null && !door.locked;
        }
    }
}