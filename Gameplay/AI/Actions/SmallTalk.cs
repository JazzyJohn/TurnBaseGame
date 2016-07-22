using UnityEngine;
using System.Collections;
using Descriptor;

namespace AI
{
    public class SmallTalk : UntilInterruptAction
    {
        public int percentOfSuccsess;
        public PerceptionValue perceptionValue;
        public int timeOfEffect;
        public bool shouldReverseMood;
        const string SUCCESS_TYPE  = "successtype";
        enum SuccessType
        {
            None,
            Mood,
            Perception
        }
        protected override bool DoAction(Context context)
        {
            PawnAI pawnAi = context.go.GetComponent<PawnAI>();
            if (pawnAi == null)
            {
                return true;
            }
            float dice = Random.Range(0,100);
            ActionData data = context.pawnAI.CreateOrAquireData<ActionData>(maker);
            if( dice < percentOfSuccsess)
            {              
         
                if((int)pawnAi.GetMood() <(int) Mood.Aggresive && pawnAi.GetMood() != Mood.Neutral)
                {
                    pawnAi.LowerMood();
                    context.AddToData(SUCCESS_TYPE, SuccessType.Mood);
                }
                else if(pawnAi.GetMood() == Mood.Neutral)
                {
                    pawnAi.PushPerception(perceptionValue);
                    context.AddToData(SUCCESS_TYPE, SuccessType.Perception);
                }

                pawnAi.GetOwner().StartTalk(TalkType.SIMPLE_RESPONDER);
                context.pawnAI.GetOwner().StartTalk(TalkType.SIMPLE_REQUESTER);
                ActionData victimData = pawnAi.CreateOrAquireData<ActionData>(maker);
                victimData.context = context;

                pawnAi.AddActionCallback(this);
                return false;
            }
            else
            {
                if ((int)pawnAi.GetMood() < (int)Mood.Aggresive )
                {
                    pawnAi.UpMood();
                }
                context.AddToData(SUCCESS_TYPE, SuccessType.None);
                pawnAi.RegisterEffect(this, context, timeOfEffect);                
                return true;
            }
            
        }
        public override bool FinishAction(AIEvent aIEvent, PawnAI pawnAI)
        {
            ActionData data = pawnAI.CreateOrAquireData<ActionData>(maker);
           
            if(data == null)
            {
                return true;
            }
            SuccessType type = (SuccessType)data.context.GetAddData(SUCCESS_TYPE);
            if (type != SuccessType.None)
            {
                data.context.pawnAI.ClearOfAnyData();
                PawnAI pawnAi = data.context.go.GetComponent<PawnAI>();
                if(pawnAi != null)
                {
                    pawnAi.ClearOfAnyData();
                }
                if (aIEvent == AIEvent.InterruptEvent && timeOfEffect > 0)
                {
                    pawnAi.RegisterEffect(this, data.context, timeOfEffect);
                }
                else
                {
                    Reverse(data.context);
                }
                pawnAi.GetOwner().StopTalk();
                data.context.pawnAI.GetOwner().StopTalk();        

            }
            return true;
        }
        public override bool CheckTarget(ActorDescriptor actorDescr)
        {
            return actorDescr.IsGuard && base.CheckTarget(actorDescr);
        }

        public override void Reverse(Context context)
        {
            SuccessType type = (SuccessType)context.GetAddData(SUCCESS_TYPE);
            PawnAI pawnAi = context.go.GetComponent<PawnAI>();
            if (pawnAi == null)
            {
                return;
            }
            switch(type)
            {
                case SuccessType.None:
                    pawnAi.LowerMood();
                    break;
                case SuccessType.Mood:
                    if(shouldReverseMood)
                    {
                        pawnAi.UpMood();
                    }
                    break;
                case SuccessType.Perception:
                    {
                        pawnAi.PopPerception();
                    }
                    break;
            }
        }

       
    }

}