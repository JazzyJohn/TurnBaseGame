using UnityEngine;
using System.Collections;

namespace AI.Conditions
{
   [CreateAssetMenu(fileName = "ParamCondition", menuName = "AI/AIConditions/ParamCondition", order = 1)]
    public class ParamCondition : NumericCondition
    {
        public CharacterParam param;


        protected override bool _CheckAI(Context context)
        {
            return CompareNumbers(context.pawnAI.GetParamsService().GetValue(param));            
        }
    }

}