using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public class EffectService : MonoBehaviour
    {
        class EffectWithDuration
        {
            public BaseAction action;
            public Context context;
            public int timeLeft;
            public EffectWithDuration(BaseAction baseAction, Context context, int timeOfEffect)
            {
                action = baseAction;
                this.context = context;
                timeLeft = timeOfEffect;
            }
        }

        List<EffectWithDuration> allEffect = new List<EffectWithDuration>();

        public void RegisterEffect(BaseAction baseAction, Context context, int timeOfEffect)
        {
            allEffect.Add(new EffectWithDuration(baseAction, context, timeOfEffect));
            allEffect.Sort(delegate(EffectWithDuration x, EffectWithDuration y)
            {
                return x.timeLeft.CompareTo(y.timeLeft);
            });
        }

        public void NewTurn()
        {
            
            allEffect.ForEach(delegate(EffectWithDuration x)
            {
                x.timeLeft--;
            });
            while(allEffect.Count > 0 && allEffect[0].timeLeft == 0)
            {
                allEffect[0].action.Reverse(allEffect[0].context);
                allEffect.RemoveAt(0);
            }
             
        }

        public int AmountOfTimeOfClosestEffect()
        {
            if (allEffect.Count > 0)
                return allEffect[0].timeLeft;
            return 0 ;
        }
    }
}