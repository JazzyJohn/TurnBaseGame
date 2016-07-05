using UnityEngine;
using System.Collections;

namespace AI
{
    public class ChangeParamsOfCharacter : BaseAction
    {
        public CharacterParam param;
        public float amount;

        public override void StartAction(PawnAI pawnAI, GameObject go)
        {
            pawnAI.ChangeParam(param, amount);
        }
        
    }
}
