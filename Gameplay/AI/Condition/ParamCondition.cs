using UnityEngine;
using System.Collections;

namespace AI.Conditions
{
   [CreateAssetMenu(fileName = "ParamCondition", menuName = "AI/AIConditions/ParamCondition", order = 1)]
    public class ParamCondition : NumericCondition
    {
        public CharacterParam param;


        protected override bool _CheckAI(PawnAI AI, GameObject target)
        {
            return CompareNumbers(AI.GetParamsService().GetValue(param));            
        }
    }

}