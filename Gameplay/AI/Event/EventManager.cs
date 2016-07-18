using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AI
{
    [System.Serializable]
    public class EventAdditionalData
    {
        public GamePlayEventType type;

        public float distance;

        public bool allowSwitchTargetInAction;
    }
    public class EventManager : MonoBehaviour
    {
        public EventAdditionalData[] eventAdditionalData;

        Dictionary<GamePlayEventType, EventAdditionalData> eventAdditionalDataMap = new Dictionary<GamePlayEventType, EventAdditionalData>();

        static EventManager sInstance;
        void Awake()
        {
            foreach (EventAdditionalData data in eventAdditionalData)
            {
                eventAdditionalDataMap[data.type] = data;
            }
            sInstance = this;
        }
        public static float GetSpreadDistance(GamePlayEventType type)
        {
            if (!sInstance.eventAdditionalDataMap.ContainsKey(type))
                return 0.0f;
            return sInstance.eventAdditionalDataMap[type].distance;
        }



        public static bool GetAllowSwitchInAction(GamePlayEventType eventType)
        {
            if (!sInstance.eventAdditionalDataMap.ContainsKey(eventType))
                return true;
            return sInstance.eventAdditionalDataMap[eventType].allowSwitchTargetInAction;
        }
    }
}
