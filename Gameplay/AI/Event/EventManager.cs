using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    [System.Serializable]
    public class EventAdditionalData
    {
        public EventType type;

        public float distance;

        public bool allowSwitchTargetInAction;
    }
    public class EventManager : MonoBehaviour
    {
        public EventAdditionalData[] eventAdditionalData;

        Dictionary<EventType, EventAdditionalData> eventAdditionalDataMap = new Dictionary<EventType, EventAdditionalData>();

        static EventManager sInstance;
        void Awake()
        {
            foreach (EventAdditionalData data in eventAdditionalData)
            {
                eventAdditionalDataMap[data.type] = data;
            }
            sInstance = this;
        }
        public static float GetSpreadDistance(EventType type)
        {
            return sInstance.eventAdditionalDataMap[type].distance;
        }



        public static bool GetAllowSwitchInAction(EventType eventType)
        {
            return sInstance.eventAdditionalDataMap[eventType].allowSwitchTargetInAction;
        }
    }
}
