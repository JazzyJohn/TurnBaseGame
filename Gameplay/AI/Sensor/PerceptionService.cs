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
    }
    public class PerceptionService : MonoBehaviour
    {
        public PerceptionValue defaultPerception;
        public Transform myTransform;

        List<DetectableObject> detectedObjects = new List<DetectableObject>();
        Stack<PerceptionValue> perceptions = new Stack<PerceptionValue>();
        // Use this for initialization
        void Start()
        {
            perceptions.Push(defaultPerception);
            if(myTransform == null)
            {
                myTransform = transform;
            }
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
                if(IsInPerception(obj))
                {
                    detectedObjects.Add(obj);
                }
            }


        }

        public bool IsInPerception(Vector3 position, GameObject go)
        {
            return _IsInPerception(perceptions.Peek(), position, go);
        }
        public bool IsInPerception(DetectableObject go)
        {
            return _IsInPerception(perceptions.Peek(), go.transform.root.position, go.gameObject);
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

    }
}
