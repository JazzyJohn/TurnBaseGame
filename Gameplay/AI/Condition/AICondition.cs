using UnityEngine;
using System.Collections;

namespace AI.Conditions
{
    public class AICondition : ScriptableObject
    {
        public bool negative;
        public bool CheckAI(Context context)
        {
            if (negative)
            {
                return !_CheckAI(context);
            }
            else
            {
                return _CheckAI(context);
            }
        }
        protected virtual bool _CheckAI(Context context)
        {
            return false;
        }
    }
}
