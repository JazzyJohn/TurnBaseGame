using UnityEngine;
using System.Collections;

namespace AI
{
    public class ChangeParamsOfCharacter : BaseAction
    {
        public CharacterParam param;
        public float amount;
        

        public override void StartAction(Context context)
        {
            if (context.allowSwitchTarget && switchTarget)
            {
                PawnAI pawnAi = context.go.GetComponent<PawnAI>();
                if( pawnAi != null)
                {
                    pawnAi.ChangeParam(param, amount);
                }
            }
            else
            {
                context.pawnAI.ChangeParam(param, amount);
            }
        }
        
    }
}
