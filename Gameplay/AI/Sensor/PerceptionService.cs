using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Actors;

namespace AI
{
    [System.Serializable]
    public class PerceptionValue
    {
        public float maxDistance;
        public float FOV;
        public bool SeeThroughWall;
        public LayerMask layerMask;
        public Mood mood;
    }
    public class PerceptionService : MonoBehaviour
    {
        public PerceptionValue defaultPerception;
        public List<PerceptionValue> moodPerceptions;
        Transform myTransform;
        PawnAI pawnAI;

        List<DetectableObject> detectedObjects = new List<DetectableObject>();
        List<PerceptionValue> perceptions = new List<PerceptionValue>();
        // Use this for initialization
        void Start()
        {
            pawnAI = GetComponent<PawnAI>();
            if (pawnAI == null)
                Debug.LogError("No AI for Perception");
            PerceptionManager.AddPerceptionService(this);
            perceptions.Add(GetForMood(Mood.Neutral));
            Debug.Log(perceptions[0]);
            if(myTransform == null)
            {
                myTransform = transform;
            }
        }
        public PerceptionValue GetCurrentValue()
        {
            return perceptions[perceptions.Count - 1];
        }
        PerceptionValue GetForMood(Mood mood)
        {
            PerceptionValue perceptionValue = moodPerceptions.Find(x => x.mood == mood);
            if(perceptionValue  == null)
            {
                if( perceptions.Count == 0)
                {
                    return defaultPerception;
                }
                else
                {
                    return GetCurrentValue();
                }
            }
            return perceptionValue;
        }

        public List<DetectableObject> GetObjectsInPerception()
        {
            return detectedObjects;
        }

        public List<DetectableObject> GetObjectsInPerception(Predicate<DetectableObject> predictate)
        {
            
            return detectedObjects.FindAll(predictate);
        }

        public void UpdateDetectList ()
        {
            detectedObjects.Clear();
            foreach(DetectableObject obj in DetectableObject.allObjects)
            {
                
                if(obj.transform.root == myTransform.root)
                {
                    continue;
                }
                if(IsInPerception(obj))
                {
                    detectedObjects.Add(obj);
                }
            }


        }

        public bool IsInPerception(Vector3 position, GameObject go)
        {
            return _IsInPerception(GetCurrentValue(), position, go);
        }
        public bool IsInPerception(DetectableObject go)
        {
            return _IsInPerception(GetCurrentValue(), go.transform.root.position, go.gameObject);
        }
        private bool _IsInPerception(PerceptionValue perceptionValue, Vector3 position, GameObject go)
        {
            if(perceptionValue == null)
            {
                return false;
            }
            Vector3 direction = position - myTransform.position;
            direction.y = 0;
            if (direction.sqrMagnitude > perceptionValue.maxDistance * perceptionValue.maxDistance)
            {
                return false;
            }

            if( Vector3.Angle(direction.normalized, myTransform.forward) >perceptionValue.FOV)
            {
                return false;
            }

            if(!perceptionValue.SeeThroughWall)
            {
                RaycastHit info;
                if (!Physics.Raycast(myTransform.position, direction.normalized, out info, direction.magnitude, perceptionValue.layerMask))
                {
                    return true;
                }
                Transform targetTransform  =  go.transform;
                if(info.collider.transform.root == targetTransform.root)
                {
                    return true;
                }

                if(info.collider.transform.root == myTransform.root)
                {

                    RaycastHit[] infos = Physics.RaycastAll(myTransform.position, direction.normalized, direction.magnitude, perceptionValue.layerMask);
                    foreach(RaycastHit infoIter in infos)
                    {
                        if(infoIter.transform.root != myTransform.root && infoIter.transform.root != targetTransform.root)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;

        }


        public void ResetPerceptionValue()
        {
            perceptions[0] = GetForMood(pawnAI.GetMood());
        }

        public void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1.0f, 0, 0.5f);
            Gizmos.DrawSphere(myTransform.position, GetCurrentValue().maxDistance);
        }

        public void PushPerception(PerceptionValue perceptionValue)
        {
            perceptions.Add(perceptionValue);
        }

        public void ClearLastPushed()
        {
            if (perceptions.Count > 1)
                perceptions.RemoveAt(perceptions.Count - 1);
        }
    }
}
