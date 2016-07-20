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
                pawnAi.StartTalk(TalkType.SIMPLE_RESPONDER);
                context.pawnAI.StartTalk(TalkType.SIMPLE_REQUESTER);
            }
            else
            {
               
                if ((int)pawnAi.GetMood() < (int)Mood.Aggresive )
                {
                    pawnAi.UpMood();
                }
            }
            
        }
        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            return actorDescr.IsGuard && base.CheckTarget(actorDescr);
        }

       
    }

}