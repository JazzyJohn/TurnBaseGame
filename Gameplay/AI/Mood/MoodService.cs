﻿using UnityEngine;
using System.Collections;
namespace AI
{
    public enum Mood
    {
        Neutral,
        Worried,
        Suspicious,
        Aggresive

    }
    public class MoodService : MonoBehaviour
    {
        PawnAI pawnAI;

        void Start()
        {
            pawnAI = GetComponent<PawnAI>();
            if(pawnAI == null)
            {
                Debug.LogError("No AI for MoodService");
            }
        }
        public Mood currentMood;
        // Use this for initialization
        public void SetMood(Mood newMood)
        {
            currentMood = newMood;
            pawnAI.MoodChanged();
        }

        public Mood GetMood()
        {
            return currentMood;
        }

        public void LowerMood()
        {
            if(currentMood == Mood.Neutral)
            {
                return;
            }
            currentMood = (Mood)((int)currentMood - 1);
            pawnAI.MoodChanged();
        }
        public void UpMood()
        {
            if (currentMood == Mood.Aggresive)
            {
                return;
            }
            currentMood = (Mood)((int)currentMood + 1);
            pawnAI.MoodChanged();
        }
    }
}
