using UnityEngine;
using System.Collections;
using System;
using Actors;
using System.Collections.Generic;

namespace AI
{
    public class PointOfInterestLogic : MonoBehaviour 
    {
        PawnAI pawnAI;

        public void Start()
        {
            pawnAI = GetComponent<PawnAI>();
            if(pawnAI == null)
            {
                Debug.LogError("NO PAwn AI ");
                enabled = false;
            }
        }
	    public void CheckForPOI()
        {
            Predicate<DetectableObject> predicate = FindPoi;
            List<DetectableObject> pois = pawnAI.GetPerceptionService().GetObjectsInPerception(predicate);
            if(pois.Count>0)
            {
                pawnAI.GetOwner().Jump();
            }
	    }

        public void OnCellChanged()
        {
            CheckForPOI();
        }
        public static bool FindPoi(DetectableObject obj)
        {
            return obj.type == DetectableObject.TYPE.POI;
        }
    }
}