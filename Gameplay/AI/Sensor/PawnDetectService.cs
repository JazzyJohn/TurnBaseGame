using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Actors;

namespace AI
{
    public class PawnDetectService : MonoBehaviour
    {
        public BaseAction actionToStart;

        PawnAI pawnAI;

        public void Start()
        {
            pawnAI = GetComponent<PawnAI>();
            if (pawnAI == null)
            {
                Debug.LogError("NO PAwn AI ");
                enabled = false;
            }
        }
        public void CheckForGuard()
        {
            Predicate<DetectableObject> predicate = FindGuard;
            List<DetectableObject> guards = pawnAI.GetPerceptionService().GetObjectsInPerception(predicate);
            if (guards.Count > 0)
            {
                Context context = new Context(pawnAI, guards[0].gameObject);
                context.allowSwitchTarget = true;
                actionToStart.StartAction(context);
            }
        }

        public void OnCellChanged()
        {
            CheckForGuard();
        }
        public static bool FindGuard(DetectableObject obj)
        {
            return obj.type == DetectableObject.TYPE.CHARACTER && obj.pawn.GetActorDescriptor().IsGuard;
        }
    }

}