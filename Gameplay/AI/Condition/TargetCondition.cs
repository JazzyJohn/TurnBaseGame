using UnityEngine;
using System.Collections;
using Descriptor;
namespace AI.Conditions
{
    public class TargetCondition : ScriptableObject
    {

        public bool negative;
        public bool Check(ActorDescriptor descr)
        {
            if (negative)
            {
                return !_Check(descr);
            }
            else
            {
                return _Check(descr);
            }
        }
        protected virtual bool _Check(ActorDescriptor descr)
        {
            return false;
        }
    }
}