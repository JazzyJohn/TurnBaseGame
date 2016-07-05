using UnityEngine;
using System.Collections;
using AI.Conditions;
using System.Collections.Generic;

namespace AI
{
    public class ConditionActionSequence : ActionSequence
    {
        public List<AICondition> conditions = new List<AICondition>();


        public override bool ShouldDoAction(int index, PawnAI pawnAi, GameObject go)
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

            return condition.CheckAI(pawnAi, go);
        }
    }
}
