﻿using UnityEngine;
using System.Collections;
namespace AI
{
    public class ChangeMood : BaseAction
    {
        public Mood mood;
        protected override bool DoAction(Context context)
        {
            MoodService moodService = null;
            if(context.allowSwitchTarget && switchTarget)
            {
                PawnAI pawnAI = context.go.GetComponent<PawnAI>();
                if(pawnAI != null)
                {
                    moodService = pawnAI.GetMoodService();                    
                }
            }
            else
            {
                moodService = context.pawnAI.GetMoodService();
            }
            if(moodService!= null)
            {
              
                moodService.SetMood(mood);
            }
            return true;
        }
       
    }
}
