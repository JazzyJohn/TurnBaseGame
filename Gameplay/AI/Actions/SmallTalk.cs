using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class SmallTalk : BaseAction
    {
        public int percentOfSuccsess;
        public PerceptionValue perceptionValue;
        protected override void _StartAction(Context context)
        {
            PawnAI pawnAi = context.go.GetComponent<PawnAI>();
            if (pawnAi == null)
            {
                return;
            }
            float dice = Random.Range(0,100);

            if( dice < percentOfSuccsess)
            {
               
                if((int)pawnAi.GetMood() <(int) Mood.Aggresive && pawnAi.GetMood() != Mood.Neutral)
                {
                    pawnAi.LowerMood();
                }
                else if(pawnAi.GetMood() == Mood.Neutral)
                {
                    pawnAi.PushPerception(perceptionValue);
                }
                pawnAi.StartTalk();
                context.pawnAI.StartTalk();
            }
            else
            {
               
                if ((int)pawnAi.GetMood() < (int)Mood.Aggresive )
                {
                    pawnAi.UpMood();
                }
            }
            
        }
        public override bool CheckTarget(ActorDescriptor actorDsecr)
        {
            return actorDsecr.IsGuard;
        }

       
    }

}