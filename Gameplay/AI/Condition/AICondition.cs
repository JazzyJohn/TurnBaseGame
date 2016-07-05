using UnityEngine;
using System.Collections;

namespace AI.Conditions
{
    public class AICondition : ScriptableObject
    {
        public bool negative;
        public bool CheckAI(PawnAI AI, GameObject target)
        {
            if (negative)
            {
                return !_CheckAI(AI,target);
            }
            else
            {
                return _CheckAI(AI, target);
            }
        }
        protected virtual bool _CheckAI(PawnAI AI, GameObject target)
        {
            return false;
        }
    }
}
