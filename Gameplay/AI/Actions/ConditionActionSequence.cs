using UnityEngine;
using System.Collections;
using AI.Conditions;
using System.Collections.Generic;

namespace AI
{
    public class ConditionActionSequence : ActionSequence
    {
        public List<AICondition> conditions = new List<AICondition>();

        public override bool CheckTarget(Descriptor.ActorDescriptor actorDescr)
        {
            for(int index = 0 ; index < actions.Length; index++) 
            {
                BaseAction action = actions[index];
                if (!action.CheckTarget(actorDescr) && conditions[index] == null)
                    return false;
            }
            return true;
        }

        public override bool ShouldDoAction(int index, Context context)
        {
            if(conditions.Count <= index)
            {
                return true;
            }
            AICondition condition = conditions[index];
            if(condition == null)
            {
                return true;
            }

            return condition.CheckAI(context);
        }
    }
}
