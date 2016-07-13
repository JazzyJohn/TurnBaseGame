using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    public class PerceptionManager : MonoBehaviour
    {
        public List<PerceptionService> allPerception = new List<PerceptionService>();

        static PerceptionManager sInstance;
        // Use this for initialization
        void Awake()
        {

            sInstance = this;
        }
        public static void AddPerceptionService(PerceptionService service)
        {
            sInstance.allPerception.Add(service);
        }
        public static void RemovePerceptionService(PerceptionService service)
        {
            sInstance.allPerception.Remove(service);
        }

        void Update()
        {
            _UpdatePerception();
        }
        void _UpdatePerception()
        {
            foreach(PerceptionService service in allPerception)
            {
                service.UpdateDetectList();
            }
        }
    }
}
