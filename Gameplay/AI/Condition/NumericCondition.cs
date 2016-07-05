using UnityEngine;
using System.Collections;

namespace AI.Conditions
{
    public enum CompareType
    {
        EQUAL,
        LESS,
        MORE,
    }
    public class NumericCondition : AICondition
    {
        public float value;
        public CompareType compareType;
        protected bool CompareNumbers(float testedValue)
        {
            switch(compareType)
            {
                case CompareType.EQUAL:
                    //TODO: ADD epsilon check
                    return testedValue == value;
                case CompareType.LESS:
                    return testedValue < value;
                case CompareType.MORE:
                    return testedValue > value;
            }
            return false;
        }
    }
}
