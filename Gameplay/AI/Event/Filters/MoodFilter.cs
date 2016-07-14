using UnityEngine;
using System.Collections;
namespace AI
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "FilterCondition", menuName = "AI/ReactionFilters/MoodFilter", order = 1)]
    public class MoodFilter : ReactionFilter
    {
        public Mood mood;
        public override bool CheckFilter(PawnAI pawnAI)
        {
           
            if (negative)
                return pawnAI.GetMood() != mood;
            else
                return pawnAI.GetMood() == mood; ;
        }
    }
}