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
        public DetectionType type;
        public enum DetectionType
        {
            Guard,
            Agent
        }

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
            Predicate<DetectableObject> predicate = null;
           
            switch(type)
            {
                case DetectionType.Agent:

                    predicate = FindAgent;
                    break;
                case DetectionType.Guard:
                    predicate = FindGuard;
                    break;
            }
            
            List<DetectableObject> guards = pawnAI.GetPerceptionService().GetObjectsInPerception(predicate);
            if (guards.Count > 0)
            {
                Context context = new Context(pawnAI, guards[0].gameObject);
                context.allowSwitchTarget = true;
                actionToStart.StartAction(context);
            }
        }

        public void Update()
        {
            CheckForGuard();
        }
        public static bool FindGuard(DetectableObject obj)
        {
            return obj.type == DetectableObject.TYPE.CHARACTER && obj.pawn.GetActorDescriptor().IsGuard;
        }
        public static bool FindAgent(DetectableObject obj)
        {
            return obj.type == DetectableObject.TYPE.CHARACTER && obj.pawn.GetActorDescriptor().IsAgent;
        }
    }

}