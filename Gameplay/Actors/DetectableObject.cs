using UnityEngine;
using System.Collections;
using PawnLogic;
using System.Collections.Generic;

namespace Actors
{
    public class DetectableObject : MonoBehaviour
    {
        public static List<DetectableObject> allObjects = new List<DetectableObject>();
        public enum TYPE
        {
            NONE,
            CHARACTER,
            POI
        };
        public TYPE type;
        [HideInInspector]
        public Pawn pawn;
        [HideInInspector]
        public Transform poi;

        void Awake()
        {
            allObjects.Add(this);
        }
        void OnDestroy()
        {
            allObjects.Remove(this);
        }
        // Use this for initialization
        void Start()
        {
            switch (type)
            {
                case TYPE.NONE:
                    pawn = GetComponent<Pawn>();
                    if(pawn!= null)
                    {
                        type = TYPE.CHARACTER;
                    }
                    else
                    {
                        poi = transform;
                        type = TYPE.POI;
                    }
                    break;
                case TYPE.CHARACTER:
                    pawn = GetComponent<Pawn>();
                    break;
                case TYPE.POI:
                    poi = transform;
                    break;
            }
           
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
